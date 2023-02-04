using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEncounterObject
{
    void Init(EncounterEventData data);

    event Action<EncounterObject, EncounterEventData> OnTriggetEvent;
}

public enum EncounterType
{
    None, 
    Water,
    Fertilizer,
    Block,
}

[Serializable]
public class EncounterEventData
{
    public EncounterType Type;
    public uint EffectValue;
}
