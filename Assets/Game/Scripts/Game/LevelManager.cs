using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameResource { }
public class GameResource : IGameResource { }

public class LevelManager : MonoBehaviour
{ 
    public EncounterManager EncounterManager;

    public void Play()
    {
        var r = new GameResource();

        EncounterManager.PrepareAll(r);
    }
}
