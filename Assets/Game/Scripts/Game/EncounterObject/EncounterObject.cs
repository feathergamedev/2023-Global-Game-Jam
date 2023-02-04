using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterObject : MonoBehaviour, IEncounterObject
{
    public event Action<EncounterObject, EncounterEventData> OnTriggetEvent;
    public Collider2D Collider2D;

    private EncounterEventData _data;

    public void Init(EncounterEventData data)
    {
        OnTriggetEvent = null;
        _data = data;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggetEvent?.Invoke(this, _data);
    }
}
