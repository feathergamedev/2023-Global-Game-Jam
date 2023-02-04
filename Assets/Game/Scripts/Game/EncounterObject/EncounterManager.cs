using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager
{
    public class EncounterObjectViewData
    {
        public EncounterObject Object;
        public EncounterEventData Data;
    }

    public EncounterObjectViewData[] ViewDatas;

    public void PrepareAll(IGameResource gameResource)
    {
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
                break;
            case EncounterType.Energy:
                break;
            case EncounterType.Block:
                break;
        }
    }
}
