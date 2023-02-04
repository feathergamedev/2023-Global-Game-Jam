using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus 
{ 
    None = 0,
    Begin,
    Grow,
    Crash,
    End
}

public enum EndType
{
    None = 0,
    TimeOut,
    WaterOut,
    EnergyOut,
    BranchOut,
}