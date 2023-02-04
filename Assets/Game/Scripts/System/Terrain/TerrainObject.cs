using System;

using JetBrains.Annotations;

using UnityEngine;

using Object = UnityEngine.Object;

public class TerrainObject
{
    public Vector2 Position { get; set; }

    private readonly GameObject _prefab;
    public EncounterType EncounterType { get; set; }
    public uint EffectValue { get; set; }

    public TerrainObject([NotNull] GameObject prefab, Vector2 position)
    {
        if (prefab == null)
        {
            throw new ArgumentNullException(nameof(prefab));
        }

        if (position.x < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "X position is invalid");
        }
        if (position.y < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "Y Position is invalid");
        }

        _prefab = prefab;
        Position = position;
    }

    public void Instantiate()
    {
        GameObject obj = Object.Instantiate(_prefab, new Vector3(Position.x, Position.y), new Quaternion());

        var component = obj.GetComponent<EncounterObject>();
        component.Init(this);
        component.OnTriggetEvent += (encounterObject, data) =>
        {
            if (encounterObject != component)
            {
                
            }
            switch (EncounterType)
            {
                case EncounterType.Water:
                    encounterObject.Consume();
                    Debug.Log("Trigger Water " + EffectValue);
                    break;
                case EncounterType.Fertilizer:
                    Object.Destroy(obj);
                    Debug.Log("Trigger Fertilizer " + EffectValue);
                    break;
                case EncounterType.Block:
                    Debug.Log("Trigger Block");
                    break;
                case EncounterType.Time:
                    Object.Destroy(obj);
                    Debug.Log("Trigger Time");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };
    }
}