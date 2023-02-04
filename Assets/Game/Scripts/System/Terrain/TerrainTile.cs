using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using UnityEngine;

public sealed class TerrainTile
{
    public int HeightPixel { get; }
    private List<TerrainObject> _objects { get; }

    public TerrainTile([NotNull] GameObject[] itemTemplates, Transform root, int widthPixel, int heightPixel)
    {
        if (itemTemplates == null)
        {
            throw new ArgumentNullException(nameof(itemTemplates));
        }

        if (widthPixel <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(widthPixel));
        }
        if (heightPixel <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(heightPixel));
        }

        HeightPixel = heightPixel;

        _objects = new List<TerrainObject>
        {
            new TerrainObject(itemTemplates[0], root, new Vector2(10, 10)),
            new TerrainObject(itemTemplates[1], root, new Vector2(20, 20)),
            new TerrainObject(itemTemplates[2], root, new Vector2(30, 30))
        };

        if (!_objects.All(o => o.Position.x <= widthPixel && o.Position.y <= heightPixel))
        {
            throw new ArgumentException($"Some of {nameof(TerrainObject)} resided in wrong position");
        }
    }

    public void Enable(Action<EncounterEventData> onTriggerEvent)
    {
        foreach (TerrainObject obj in _objects)
        {
            obj.OnCollidedEvent += onTriggerEvent;
            obj.Instantiate();
        }
    }
}