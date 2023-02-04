using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterObject : MonoBehaviour, IEncounterObject
{
    public event Action<EncounterObject, EncounterEventData> OnTriggetEvent;

    public Collider2D Collider2D;
    public SpriteRenderer SpriteRenderer;

    private EncounterEventData _data;
    private bool _enable = false; 

    public void Init(EncounterEventData data)
    {
        OnTriggetEvent = null;
        _data = data;
        _enable = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (_enable)
        {
            OnTriggetEvent?.Invoke(this, _data);
        }
    }

    public void Remove()
    {
        _enable = false;
        Destroy(this.gameObject);
    }

    public void Consume()
    {
        _enable = false;
        var color = SpriteRenderer.color;
        color.a = 0.3f;
        SpriteRenderer.color = color;
    }
}
