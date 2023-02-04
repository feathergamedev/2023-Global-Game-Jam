using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public enum EncounterType
{
    None, 
    Water,
    Fertilizer,
    Block,
    Time
}

[Serializable]
public class EncounterEventData
{
    public EncounterType Type;
    public uint EffectValue;
}

public sealed class EncounterManager : MonoBehaviour
{
    public event Action OnRootCrash;

    public GameObject[] ItemTemplates;

    public GameObject _dirt;

    private float _yCameraDelta;
    private List<TerrainTile> _existedTiles = new List<TerrainTile>();

    [Serializable]
    public class EncounterObjectViewData
    {
        public EncounterObject Object;
        public EncounterEventData Data;
    }
    
    private ResourceTracker _resourceTracker;

    public void PrepareAll(ResourceTracker resourceTracker, CameraManager cameraManager)
    {
        _resourceTracker = resourceTracker;

        _existedTiles.Add(CreateNewTerrainTile());
        cameraManager.OnPositionYChanged += yChangeAmount =>
        {
            _yCameraDelta += yChangeAmount;
            if (_existedTiles.Sum(t => t.HeightPixel) < _yCameraDelta + 100)
            {
                _existedTiles.Add(CreateNewTerrainTile());
            }
        };
            
        TerrainTile CreateNewTerrainTile()
        {
            var terrainTile = new TerrainTile(ItemTemplates, 1920, 1080);
            terrainTile.Enable(OnTriggerEvent);
            return terrainTile;
        }
        
        void OnTriggerEvent(EncounterEventData data)
        {
            switch (data.Type)
            {
                case EncounterType.Water:
                    _resourceTracker.IncreaseWater(data.EffectValue);
                    break;
                case EncounterType.Fertilizer:
                    _resourceTracker.IncreaseFertilizer(data.EffectValue);
                    break;
                case EncounterType.Block:
                    OnRootCrash?.Invoke();
                    break;
                case EncounterType.Time:
                    _resourceTracker.IncreaseTime(data.EffectValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
