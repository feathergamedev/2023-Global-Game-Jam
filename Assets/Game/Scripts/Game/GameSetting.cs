using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public ulong InitialLifeSeconds;
    public uint InitialWater;
    public uint InitialEnergy;
    public uint InitialFertilizer;
    public uint InitialUsableBranches;

    public uint WaterLimit;
    public uint EnergyLimit;
    public uint FertilizeLimit;

    public uint ConsumeWaterPerSecond;
    public uint ConsumeFertilizerPerSecond;
    public uint ConsumeEnergyPerTime;

    public uint RecoverEnergyPerSecond;
    public uint BonusRecoverEnergyPerSecond;

    public int MinEventCountPerTile;
    public int MaxEventCountPerTile;
    public float MinDistanceEachEvent;
    public float OriginProtectDistance;

    public int RandomWeightWater;
    public int RandomWeightFertilizer;
    public int RandomWeightBlock;
    public int RandomWeightTime;
}


