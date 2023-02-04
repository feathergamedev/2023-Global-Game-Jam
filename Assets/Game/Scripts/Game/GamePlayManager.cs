using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePlayManager : MonoBehaviour
{
    public GameSetting GameSetting;
    public EncounterManager EncounterManager;
    public RootController RootController;

    private ResourceTracker ResourceTracker;

    public GameStatus Status;
    public EndType EndType;

    async void Start()
    {
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

        EncounterManager.PrepareAll(ResourceTracker);
        EncounterManager.OnRootCrash += _OnRootCrash;

        RootController.OnGrowAction += _OnRootAction;
        RootController.OnRootCrash += _OnRootCrash;

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
                if (Status == GameStatus.Grow || Status == GameStatus.Crash)
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
        await UniTask.NextFrame();
        Status = GameStatus.Grow;
    }

    private async UniTask GamePlayMain()
    {
        while (Status != GameStatus.End)
        {
            if (Status == GameStatus.Grow)
            {
            }
            else if (Status == GameStatus.Crash)
            {
            }

            await UniTask.NextFrame();
        }
    }

    private async UniTask DisplayEnd()
    {
        await UniTask.NextFrame();
    }
    #endregion

    #region GameEvent
    private void _ResourceUpdatePerSecond()
    {
        ResourceTracker.DecreaseTime(1);
        if (ResourceTracker.Time == 0)
        {
            _OnTimeOut();
            return;
        }

        ResourceTracker.DecreaseWater(GameSetting.ConsumeWaterPerSecond);
        if (ResourceTracker.Water == 0)
        {
            _OnWaterOut();
            return;
        }

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
        if (ResourceTracker.Energy == 0)
        {
            _OnEnergyOut();
            return;
        }
    }

    private void _OnRootCrash()
    {
        ResourceTracker.DecreaseBranch(1);
        if (ResourceTracker.Branch == 0)
        {
            Status = GameStatus.End;
            Debug.Log("Status => End ");
        }
        else
        {
            Status = GameStatus.Crash;
            Debug.Log("Status => Crash ");
        }
    }

    private void _OnTimeOut()
    {
        Status = GameStatus.End;
        EndType = EndType.TimeOut;
    }

    private void _OnWaterOut()
    {
        Status = GameStatus.End;
        EndType = EndType.WaterOut;
    }

    private void _OnEnergyOut()
    {
        Status = GameStatus.End;
        EndType = EndType.EnergyOut;
    }
    #endregion
}
