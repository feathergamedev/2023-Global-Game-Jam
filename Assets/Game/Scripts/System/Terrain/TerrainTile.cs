using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using UnityEngine;

public sealed class TerrainTile
{
    private int _widthPixel;
    private int _heightPixel;

    public List<TerrainObject> Objects { get; }

    public TerrainTile([NotNull] GameObject[] itemTemplates, int widthPixel, int heightPixel)
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

        _widthPixel = widthPixel;
        _heightPixel = heightPixel;
        
        Objects = new List<TerrainObject>
        {
            new TerrainObject(itemTemplates[0], new Vector2(10, 10)),
            new TerrainObject(itemTemplates[1], new Vector2(20, 20)),
            new TerrainObject(itemTemplates[2], new Vector2(30, 30)),
            new TerrainObject(itemTemplates[3], new Vector2(40, 40))
        };

        if (!Objects.All(o => o.Position.x <= _widthPixel && o.Position.y <= _heightPixel))
        {
            throw new ArgumentException($"Some of {nameof(TerrainObject)} resided in wrong position");
        }
    }
}