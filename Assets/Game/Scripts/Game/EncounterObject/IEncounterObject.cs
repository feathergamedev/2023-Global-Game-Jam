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
    Energy,
    Block,
}

public struct EncounterEventData
{
    public EncounterType Type;
    public string EffectValue;
}

public class EncounterObject : MonoBehaviour, IEncounterObject
{
    public event Action<EncounterObject, EncounterEventData> OnTriggetEvent;
    public Collider Collider2D;

    private EncounterEventData _data;

    public void Init(EncounterEventData data)
    {
        OnTriggetEvent = null;
        _data = data;
    }

    public void OnCollisionEnter(Collision collision)
    {
        OnTriggetEvent?.Invoke(this, _data);
    }
}
