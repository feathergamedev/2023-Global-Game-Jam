using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lapis.Utilities;

public class SceneTransitionManager : MonoSingleton<SceneTransitionManager>
{

    private Dictionary<SceneType, string> _sceneNameDictionary = new Dictionary<SceneType, string>
    {
        {SceneType.Entry, "EntryScene"},
        {SceneType.Home, "HomeScene"},
        {SceneType.Game, "GameScene"},
        {SceneType.Credit, "CreditScene"}
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene(SceneType nextSceneType)
    {
        SceneManager.LoadScene(_sceneNameDictionary[nextSceneType]);
    }
}
