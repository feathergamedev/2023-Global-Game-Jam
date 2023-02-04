using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
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
                Debug.Log("Trigger Water " + data.EffectValue);
                break;
            case EncounterType.Energy:
                _resourceTracker.IncreaseWater(data.EffectValue);
                Debug.Log("Trigger Energy " + data.EffectValue);
                break;
            case EncounterType.Block:
                Debug.Log("Trigger Block");
                break;
        }
    }
}
