using System;

using JetBrains.Annotations;

using UnityEngine;

public class TerrainObject
{
    private readonly EncounterObject _encounterObject;

    public TerrainObject([NotNull] GameObject obj)
    {
        _encounterObject = obj.GetComponent<EncounterObject>();
        if (_encounterObject == null)
        {
            throw new ArgumentException($"Did not have {nameof(EncounterObject)} component", nameof(obj));
        }
    }

    public event Action<EncounterEventData> OnCollidedEvent;

    public void Initialize() { _encounterObject.Init(data => { OnCollidedEvent?.Invoke(data); }); }
}