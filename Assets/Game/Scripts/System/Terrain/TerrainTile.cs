using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Object = UnityEngine.Object;

public sealed class TerrainTile
{
    public TerrainTile
        (Transform root, GameObject dirtTilePrefab, float yOffset, LevelMapGenerator levelMapGenerator)
    {
        GameObject newTile = Object.Instantiate(dirtTilePrefab, root);
        newTile.transform.position = new Vector2(newTile.transform.position.x, yOffset);
        List<LevelMapGenerator.EnounterObjectPos> newObjs = levelMapGenerator.GenerateEncounterEvents(newTile.transform);
        _objects = newObjs.Select(o => new TerrainObject(o.EnounterGameObject)).ToList();
    }

    private List<TerrainObject> _objects { get; }

    public void Enable(Action<EncounterEventData> onTriggerEvent)
    {
        foreach (TerrainObject obj in _objects)
        {
            obj.OnCollidedEvent += onTriggerEvent;
            obj.Initialize();
        }
    }
}