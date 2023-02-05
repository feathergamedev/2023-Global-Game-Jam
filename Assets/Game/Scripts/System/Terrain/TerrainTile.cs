using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using UnityEngine;

public sealed class TerrainTile
{
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
            new TerrainObject(itemTemplates[0], root, new Vector2(0.1f, 0.1f)),
            new TerrainObject(itemTemplates[1], root, new Vector2(0.2f, 0.2f)),
            new TerrainObject(itemTemplates[2], root, new Vector2(0.3f, 0.3f))
        };

        if (!_objects.All(o => o.Position.x <= widthPixel && o.Position.y <= heightPixel))
        {
            throw new ArgumentException($"Some of {nameof(TerrainObject)} resided in wrong position");
        }
    }

    public int HeightPixel { get; }
    private List<TerrainObject> _objects { get; }

    public void Enable(Action<EncounterEventData> onTriggerEvent)
    {
        foreach (TerrainObject obj in _objects)
        {
            obj.OnCollidedEvent += onTriggerEvent;
            obj.Instantiate();
        }
    }
}