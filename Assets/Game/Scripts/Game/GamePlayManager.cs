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
    private GameStatus Status;

    async void Start()
    {
        Play();
    }

    public async void Play()
    {
        ResourceTracker = new ResourceTracker(
            GameSetting.InitialLifeSeconds,
            GameSetting.InitialWater,
            GameSetting.InitialUsableBranches);

        EncounterManager.PrepareAll(ResourceTracker);
         
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
                Debug.Log("time updating... "); 
            }

            await UniTask.NextFrame();
        }
    }

    private async UniTask GamePlayTask()
    {
        Status = GameStatus.Begin;
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
    { /*
        ResourceTracker.DecreaseWater(GameSetting.ConsumeWaterPerSecond);

        if (ResourceTracker.Fertilizer > 0)
        {
            ResourceTracker.DecreaseFertilizer(GameSetting.ConsumeFertilizerPerSecond);
            ResourceTracker.IncreaseEnergy(GameSetting.BonusRecoverEnergyPerSecond);
        }

        ResourceTracker.IncreaseEnergy(GameSetting.RecoverEnergyPerSecond);
        */

        Debug.Log($"GameStatus[{Status}]  Water[{0}] Energy[{1}] Fertilizer[{2}]");
    }

    private void _OnRootAction()
    {
        /*
        ResourceTracker.DecreaseEnergy(GameSetting.ConsumeEnergyPerTime);
        */
    }

    private void _OnRootCrash()
    {
        /*
        ResourceTracker.DecreaseBranches();
        if (ResourceTracker.Branches == 0)
        {
            Status = GameStatus.End;
            Debug.Log("Status => End ");
        }
        else
        {
            Status = GameStatus.Crash;
            Debug.Log("Status => Crash ");
        }
        */
    }
    #endregion
}
