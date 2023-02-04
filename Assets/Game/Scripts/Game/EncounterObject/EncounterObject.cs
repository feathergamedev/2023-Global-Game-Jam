using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EncounterObject : MonoBehaviour, IEncounterObject
{
    public event Action<EncounterObject, EncounterEventData> OnTriggetEvent;

    public Collider2D Collider2D;
    public SpriteRenderer SpriteRenderer;
    public EncounterEventData EncounterEventData;

    private EncounterEventData _data;
    private bool _enable;

    public void Init(TerrainObject obj)
    {
        obj.EncounterType = EncounterEventData.Type;
        obj.EffectValue = EncounterEventData.EffectValue;
    }

    public void Init(EncounterEventData data)
    {
        OnTriggetEvent = null;
        _data = data;
        _enable = true;
    }

    public void Consume()
    {
        _enable = false;
        var color = SpriteRenderer.color;
        color.a = 0.3f;
        SpriteRenderer.color = color;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_enable)
        {
            OnTriggetEvent?.Invoke(this, _data);
        }
    }
}
