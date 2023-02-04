using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public event Action OnRootCrash;

    [Serializable]
    public class EncounterObjectViewData
    {
        public EncounterObject Object;
        public EncounterEventData Data;
    }
    
    public List<EncounterObjectViewData> ViewDatas;
    private ResourceTracker _resourceTracker;

    public void PrepareAll(ResourceTracker resourceTracker)
    {
        _resourceTracker = resourceTracker;
        foreach (var viewData in ViewDatas)
        {
            viewData.Object.Init(viewData.Data);
            viewData.Object.OnTriggetEvent += _OnTriggerEvent;
        }
    }

    private void _OnTriggerEvent(EncounterObject encounterObject, EncounterEventData data)
    {
        switch (data.Type)
        {
            case EncounterType.Water:
                _resourceTracker.IncreaseWater(data.EffectValue);
                encounterObject.Consume();
                Debug.Log("Trigger Water " + data.EffectValue);
                break;
            case EncounterType.Fertilizer:
                _resourceTracker.IncreaseFertilizer(data.EffectValue);
                encounterObject.Remove();
                Debug.Log("Trigger Fertilizer " + data.EffectValue);
                break;
            case EncounterType.Block:
                OnRootCrash?.Invoke();
                Debug.Log("Trigger Block");
                break;
            case EncounterType.Time:
                _resourceTracker.IncreaseTime(data.EffectValue);
                encounterObject.Remove();
                Debug.Log("Trigger Time");
                break;
        }
    }
}
