using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapGenerator : MonoBehaviour
{
    public GameObject WaterCandidate;
    public GameObject FertilizerCandidate;
    public GameObject BlockCandidate;

    private Bounds _waterBounds;
    private Bounds _fertilizerBounds;
    private Bounds _blockBounds;
    private int _minCounts;
    private int _maxCounts;

    public void Init(GameSetting gameSetting)
    {
        _waterBounds = WaterCandidate.GetComponent<Collider2D>().bounds;
        _fertilizerBounds = FertilizerCandidate.GetComponent<Collider2D>().bounds;
        _blockBounds = BlockCandidate.GetComponent<Collider2D>().bounds;

        _minCounts = 3;
        _maxCounts = 10;
    }

    public void GenerateEncounterEvents(int widthPixel, int heightPixel)
    {
        int counts = Random.Range(_minCounts, _maxCounts);
        var points = new List<Vector2>();

        for (var i = 0; i < counts; i++)
        {
            var point = new Vector2(Random.value * widthPixel, Random.value * heightPixel);

        }
    }
}
