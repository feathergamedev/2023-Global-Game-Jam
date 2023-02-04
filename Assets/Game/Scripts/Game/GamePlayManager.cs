using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GamePlayManager : MonoBehaviour
{ 
    public EncounterManager EncounterManager;

    void Start()
    {
        Play();
    }

    public void Play()
    { 
        var resourceTracker = new ResourceTracker(100, 100, 100);

        EncounterManager.PrepareAll(resourceTracker);
    }
}
