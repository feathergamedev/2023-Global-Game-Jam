using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEncounterObject
{
    event Action<EncounterObject, EncounterEventData> OnTriggetEvent;

    void Init(EncounterEventData data);
    void Remove();
    void Consume();
}

public enum EncounterType
{
    None, 
    Water,
    Fertilizer,
    Block,
    Time
}

[Serializable]
public class EncounterEventData
{
    public EncounterType Type;
    public uint EffectValue;
}
