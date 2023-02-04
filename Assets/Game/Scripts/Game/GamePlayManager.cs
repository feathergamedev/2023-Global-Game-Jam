using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GamePlayManager : MonoBehaviour
{
    public GameSetting GameSetting;
    public EncounterManager EncounterManager;
    public RootController RootController;
    public CameraManager CameraManager;
    public GamePlayPanel GamePlayPanel;

    [SerializeField] private CanvasGroup GameplayUi;

    private ResourceTracker ResourceTracker;

    public GameStatus Status;
    public EndType EndType;

    async void Start()
    {
        GameplayUi.alpha = 0;
        Play();
    }

    public async void Play()
    {
        ResourceTracker = new ResourceTracker(
              time: new ResourceSetting<ulong>(0, ulong.MaxValue, GameSetting.InitialLifeSeconds),
              energy: new ResourceSetting<uint>(0, GameSetting.EnergyLimit, GameSetting.InitialEnergy),
              water: new ResourceSetting<uint>(0, GameSetting.WaterLimit, GameSetting.InitialWater),
              fertilizer: new ResourceSetting<uint>(0, GameSetting.FertilizeLimit, GameSetting.InitialFertilizer),
              branches: new ResourceSetting<uint>(0, GameSetting.InitialUsableBranches, GameSetting.InitialUsableBranches));

        ResourceTracker.ResourceExhausted += _OnResourceExhausted;

        EncounterManager.PrepareAll(ResourceTracker);
        EncounterManager.OnRootCrash += _OnRootCrash;

        RootController.OnGrowAction += _OnRootAction;
        RootController.OnRootCrash += _OnRootCrash;

        GamePlayPanel.Init(ResourceTracker, GameSetting);

        await UniTask.WhenAll(GamePlayTask(), TimerTask());
    }

    private async UniTask TimerTask()
    {
        float time = 0;

        while (true)
        {
            time += UnityEngine.Time.deltaTime;

            if (time > 1)
            {
                time -= 1;
                if (Status == GameStatus.Grow)
                {
                    _ResourceUpdatePerSecond();
                }
            }

            await UniTask.NextFrame();
        }
    }

    private async UniTask GamePlayTask()
    {
        Status = GameStatus.Begin;
        EndType = EndType.None;

        await DisplayBegin();

        await GamePlayMain();

        await DisplayEnd();
    }

    #region GameStatus
    private async UniTask DisplayBegin()
    {
        await CameraManager.EnterStageCameraPerform();
        Status = GameStatus.Grow;
        DOTween.To(() => GameplayUi.alpha, x => GameplayUi.alpha = x, 1f, 0.5f);

        await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f));
    }

    private async UniTask GamePlayMain()
    {
        RootController.StartGrow();
        GamePlayPanel.ShowPanel();
        while (Status != GameStatus.End)
        {
            if (Status == GameStatus.Grow)
            {
            }
            else if (Status == GameStatus.Crash)
            {
                await RootController.SwitchBranch();
                Status = GameStatus.Grow;
            }

            await UniTask.NextFrame();
        }
        RootController.StopGrow();
        GamePlayPanel.HidePanel();
    }

    private async UniTask DisplayEnd()
    {
        TierComputer.Tier tier = TierComputer.Run(ResourceTracker);
        Debug.Log($"Final tier {tier.ToString()}"); 
        await UniTask.NextFrame();
    }
    #endregion

    #region GameEvent
    private void _ResourceUpdatePerSecond()
    {
        ResourceTracker.DecreaseTime(1); 

        ResourceTracker.DecreaseWater(GameSetting.ConsumeWaterPerSecond); 

        if (ResourceTracker.Fertilizer > 0)
        {
            ResourceTracker.DecreaseFertilizer(GameSetting.ConsumeFertilizerPerSecond);
            ResourceTracker.IncreaseEnergy(GameSetting.BonusRecoverEnergyPerSecond);
        }

        ResourceTracker.IncreaseEnergy(GameSetting.RecoverEnergyPerSecond);

        Debug.Log($"GameStatus[{Status}] " +
            $"Time[{ResourceTracker.Time}] " +
            $"Water[{ResourceTracker.Water}] " +
            $"Energy[{ResourceTracker.Energy}] " +
            $"Fertilizer[{ResourceTracker.Fertilizer}]");
    }

    private void _OnRootAction(int length)
    {
        ResourceTracker.DecreaseEnergy(GameSetting.ConsumeEnergyPerTime);
    }

    private void _OnRootCrash()
    {
        ResourceTracker.DecreaseBranch(1);
        if (ResourceTracker.Branch > 0) 
        {
            Status = GameStatus.Crash;
            Debug.Log("Status => Crash ");
        }
    }

    private void _OnResourceExhausted(object sender, ResourceExhaustedEventArgs args)
    {
        switch (args.Resource)
        {
            case TimeResource time:
                Status = GameStatus.End;
                EndType = EndType.TimeOut;
                Debug.Log("Status => End : TimeOut");
                break;
            case WaterResource water:
                Status = GameStatus.End;
                EndType = EndType.WaterOut;
                Debug.Log("Status => End : WaterOut");
                break;
            case EnergyResource energy:
                Status = GameStatus.End;
                EndType = EndType.EnergyOut;
                Debug.Log("Status => End : EnergyOut");
                break;
            case BranchResource branch:
                Status = GameStatus.End;
                EndType = EndType.BranchOut;
                Debug.Log("Status => End : BranchOut");
                break;
        }
    }
    #endregion
}
