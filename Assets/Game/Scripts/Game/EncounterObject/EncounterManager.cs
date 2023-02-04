using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EncounterManager : MonoBehaviour
{
    public event Action OnRootCrash;

    public GameObject[] ItemTemplates;

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
        // Create multiple tiles
        var tile = new TerrainTile(ItemTemplates, 1920, 1080);
        
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
                break;
            case EncounterType.Fertilizer:
                _resourceTracker.IncreaseFertilizer(data.EffectValue);
                break;
            case EncounterType.Block:
                OnRootCrash?.Invoke();
                break;
            case EncounterType.Time:
                _resourceTracker.IncreaseTime(data.EffectValue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
