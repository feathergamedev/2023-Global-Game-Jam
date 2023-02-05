using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapGenerator : MonoBehaviour
{
    public struct EnounterBoundPair
    {
        public GameObject EnounterGameObject;
        public Bounds Bounds;
    }

    public struct EnounterObjectPos
    {
        public GameObject EnounterGameObject;
        public Vector3 Position;
    }

    public List<GameObject> WaterCandidates;
    public List<GameObject> FertilizerCandidates;
    public List<GameObject> BlockCandidates;
    public List<GameObject> TimeCandidates;
    private List<EncounterType> EncounterTypeList = new List<EncounterType> {
        EncounterType.Water,
        EncounterType.Fertilizer,
        EncounterType.Block,
        EncounterType.Time
    };

    private Dictionary<EncounterType, List<EnounterBoundPair>> _encounterBoundTable = new Dictionary<EncounterType, List<EnounterBoundPair>>();
    private Dictionary<EncounterType, int> _encounterTypeWeightTable = new Dictionary<EncounterType, int>();
    private int _weightMax;

    private const int TRY_LIMIT = 1000;
    private GameSetting _gameSetting;
    
    public void Init(GameSetting gameSetting)
    {
        _gameSetting = gameSetting;

        _encounterBoundTable[EncounterType.Water] = new List<EnounterBoundPair>();
        foreach (var waterObject in WaterCandidates)
        {
            var bounds = waterObject.GetComponent<SpriteRenderer>().bounds;
            Debug.Log($"EncounterType.Water bounds[{bounds}]");
            _encounterBoundTable[EncounterType.Water].Add(new EnounterBoundPair {
                EnounterGameObject = waterObject,
                Bounds = new Bounds(Vector3.zero, bounds.size)
            });
        }

        _encounterBoundTable[EncounterType.Fertilizer] = new List<EnounterBoundPair>();
        foreach (var fertilizerObject in FertilizerCandidates)
        {
            var bounds = fertilizerObject.GetComponent<SpriteRenderer>().bounds;
            Debug.Log($"EncounterType.Fertilizer bounds[{bounds}]");
            _encounterBoundTable[EncounterType.Fertilizer].Add(new EnounterBoundPair
            {
                EnounterGameObject = fertilizerObject,
                Bounds = new Bounds(Vector3.zero, bounds.size)
            });
        }

        _encounterBoundTable[EncounterType.Block] = new List<EnounterBoundPair>();
        foreach (var blockObject in BlockCandidates)
        {
            var bounds = blockObject.GetComponent<SpriteRenderer>().bounds;
            Debug.Log($"EncounterType.Block bounds[{bounds}]");
            _encounterBoundTable[EncounterType.Block].Add(new EnounterBoundPair
            {
                EnounterGameObject = blockObject,
                Bounds = new Bounds(Vector3.zero, bounds.size)
            });
        }

        _encounterBoundTable[EncounterType.Time] = new List<EnounterBoundPair>();
        foreach (var timeObject in TimeCandidates)
        {
            var bounds = timeObject.GetComponent<SpriteRenderer>().bounds;
            Debug.Log($"EncounterType.Block bounds[{bounds}]");
            _encounterBoundTable[EncounterType.Time].Add(new EnounterBoundPair
            {
                EnounterGameObject = timeObject,
                Bounds = new Bounds(Vector3.zero, bounds.size)
            });
        }

        _weightMax = 0;
        _weightMax += _gameSetting.RandomWeightWater;
        _encounterTypeWeightTable[EncounterType.Water] = _weightMax;
        _weightMax += _gameSetting.RandomWeightFertilizer;
        _encounterTypeWeightTable[EncounterType.Fertilizer] = _weightMax;
        _weightMax += _gameSetting.RandomWeightBlock;
        _encounterTypeWeightTable[EncounterType.Block] = _weightMax;
        _weightMax += _gameSetting.RandomWeightTime;
        _encounterTypeWeightTable[EncounterType.Time] = _weightMax;
    }

    public List<EnounterObjectPos> GenerateEncounterEvents(Transform root)
    {
        var rootRenderer = root.GetComponent<SpriteRenderer>();
        var rootBounds = rootRenderer.bounds;
        var rootWidth = rootBounds.size.x;
        var rootHeight = rootBounds.size.y;
        Debug.Log($"root bounds[{rootRenderer.bounds}]  rootWidth[{rootWidth}]  rootHeight[{rootHeight}]");
        var playerBeginPoint = new Vector2(rootWidth/2, rootHeight);

        int goalCounts = Random.Range(_gameSetting.MinEventCountPerTile, _gameSetting.MaxEventCountPerTile);
        int tryCount = 0;
        var encounterBounds = new List<(GameObject, Bounds)>();
        var encounterPositions = new List<EnounterObjectPos>();

        while (encounterBounds.Count < goalCounts && tryCount < TRY_LIMIT)
        {
            tryCount++;

            var randType = _GetRandomEncounterType();
            Debug.Log($"new randType[{randType}]");
            var encounterTargetPair = _SelectRandomEncounter(randType);
            var newPoint = new Vector2(Random.value * rootWidth, Random.value * rootHeight);
            var newBound = new Bounds(newPoint, encounterTargetPair.Bounds.size);
            Debug.Log($"add newPoint[{newPoint}]newBound[{newBound}]");
            if (newBound.min.x < 0 || newBound.min.y < 0 ||
                newBound.max.x > rootWidth || newBound.max.y > rootHeight)
            {
                Debug.Log($"check newPoint[{newPoint}]newBound[{newBound}] OUT");
                continue; 
            }
            if (Vector2.Distance(newPoint, playerBeginPoint) < _gameSetting.OriginProtectDistance)
            {
                Debug.Log($"check newPoint[{newPoint}]newBound[{newBound}] TooOrigin");
                continue;
            }

            var isClosed = false;
            for (var k = 0; k < encounterBounds.Count; k++)
            {
                var prevBound = encounterBounds[k];
                var distance = Vector2.Distance(newPoint, prevBound.Item2.center);
                if (distance < _gameSetting.MinDistanceEachEvent)
                {
                    Debug.Log($"check prevBounds[{prevBound.Item2}] TooClosed");
                    isClosed = true;
                    break;
                }

                if (prevBound.Item2.Contains(newPoint))
                {
                    Debug.Log($"check prevBounds[{prevBound.Item2}] HIT");
                    isClosed = true;
                    break;
                }
            }

            if (isClosed)
                continue;

            encounterBounds.Add((encounterTargetPair.EnounterGameObject, newBound));
        }

        var basicPos = transform.position;
        Debug.Log($"root basicPos[{basicPos}]");
        foreach (var bound in encounterBounds)
        {
            var pos = bound.Item2.center;
            Debug.Log($"basic pos [{pos}]");
            pos.x += -rootWidth/2;
            pos.y += -rootHeight/2;
            Debug.Log($"set go to pos [{pos}]");
            var go = Instantiate(bound.Item1, root);
            go.transform.localPosition = pos; 

            encounterPositions.Add(new EnounterObjectPos {
                EnounterGameObject = go,
                Position = pos
            });
        }

        return encounterPositions;
    }

    private EnounterBoundPair _SelectRandomEncounter(EncounterType encounterType)
    {
        var candidateList = _encounterBoundTable[encounterType];
        Debug.Log($"candidateList[{candidateList.Count}]");
        var randPairNum = Random.Range(0, candidateList.Count);
        var randPair = candidateList[randPairNum];
        return randPair;
    }

    private EncounterType _GetRandomEncounterType()
    {
        var randWeight = Random.Range(0, _weightMax+1);
        Debug.Log($"randWeight[{randWeight}]");
        if (randWeight < _encounterTypeWeightTable[EncounterType.Water])
            return EncounterType.Water;
        if (randWeight < _encounterTypeWeightTable[EncounterType.Fertilizer])
            return EncounterType.Fertilizer;
        if (randWeight < _encounterTypeWeightTable[EncounterType.Block])
            return EncounterType.Block;
        return EncounterType.Time;
    }
}
