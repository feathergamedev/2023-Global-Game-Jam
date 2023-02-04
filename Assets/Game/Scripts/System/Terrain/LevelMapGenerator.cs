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

    public List<GameObject> WaterCandidates;
    public List<GameObject> FertilizerCandidates;
    public List<GameObject> BlockCandidates;
    private List<EncounterType> EncounterTypeList = new List<EncounterType> {
        EncounterType.Water,
        EncounterType.Fertilizer,
        EncounterType.Block
    };

    private Dictionary<EncounterType, List<EnounterBoundPair>> EncounterBoundTable = new Dictionary<EncounterType, List<EnounterBoundPair>>();

    private int _minCounts;
    private int _maxCounts;
    private const int TRY_LIMIT = 1000;
    
    public void Init(GameSetting gameSetting)
    {
        EncounterBoundTable[EncounterType.Water] = new List<EnounterBoundPair>();
        foreach (var waterObject in WaterCandidates)
        {
            var bounds = waterObject.GetComponent<SpriteRenderer>().bounds;
            Debug.Log($"EncounterType.Water bounds[{bounds}]");
            EncounterBoundTable[EncounterType.Water].Add(new EnounterBoundPair {
                EnounterGameObject = waterObject,
                Bounds = new Bounds(Vector3.zero, bounds.size)
            });
        }

        EncounterBoundTable[EncounterType.Fertilizer] = new List<EnounterBoundPair>();
        foreach (var fertilizerObject in FertilizerCandidates)
        {
            var bounds = fertilizerObject.GetComponent<SpriteRenderer>().bounds;
            Debug.Log($"EncounterType.Fertilizer bounds[{bounds}]");
            EncounterBoundTable[EncounterType.Fertilizer].Add(new EnounterBoundPair
            {
                EnounterGameObject = fertilizerObject,
                Bounds = new Bounds(Vector3.zero, bounds.size)
            });
        }

        EncounterBoundTable[EncounterType.Block] = new List<EnounterBoundPair>();
        foreach (var blockObject in BlockCandidates)
        {
            var bounds = blockObject.GetComponent<SpriteRenderer>().bounds;
            Debug.Log($"EncounterType.Block bounds[{bounds}]");
            EncounterBoundTable[EncounterType.Block].Add(new EnounterBoundPair
            {
                EnounterGameObject = blockObject,
                Bounds = new Bounds(Vector3.zero, bounds.size)
            });
        }

        _minCounts = 3;
        _maxCounts = 4;
    }

    public void GenerateEncounterEvents(int widthPixel, int heightPixel)
    {
        int goalCounts = Random.Range(_minCounts, _maxCounts);
        int tryCount = 0;
        var encounterPairs = new List<EnounterBoundPair>();

        while (encounterPairs.Count < goalCounts && tryCount < TRY_LIMIT)
        {
            tryCount++;

            var encounterTargetPair = _SelectRandomEncounter();
            var newPoint = new Vector2(Random.value * widthPixel, Random.value * heightPixel);
            Debug.Log($"try newpoint[{newPoint}]");

            var isClosed = false;
            for (var k = 0; k < encounterPairs.Count; k++)
            {
                var prevPair= encounterPairs[k];
                if (prevPair.Bounds.Contains(newPoint))
                { 
                    Debug.Log($"check prevBounds[{prevPair.Bounds}] HIT");
                    isClosed = true;
                    break;
                }
            }

            if (isClosed)
                continue;

            var newBound = new Bounds(newPoint, encounterTargetPair.Bounds.size);
            Debug.Log($"add new Bounds[{newBound}]");
            encounterPairs.Add(new EnounterBoundPair {
                EnounterGameObject = encounterTargetPair.EnounterGameObject,
                Bounds = newBound
            });
        }
    }

    private EnounterBoundPair _SelectRandomEncounter()
    {
        var randTypeNum = Random.Range(0, EncounterTypeList.Count);
        Debug.Log($"randTypeNum[{randTypeNum}]");
        var randType = EncounterTypeList[randTypeNum];
        Debug.Log($"new randType[{randType}]");

        var candidateList = EncounterBoundTable[randType];
        Debug.Log($"candidateList[{candidateList.Count}]");
        var randPairNum = Random.Range(0, candidateList.Count);
        var randPair = candidateList[randPairNum];
        return randPair;
    }
}