using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    Entry,
    Home,
    Game,
    Credit
}

public static class Config
{
    // Product Info
    public const string teamName = "Global Game Jam 2023 Taipei \n Team ?";
    public const string audioSourcePrefabName = "AudioSourcePrefab";

    // Scene
    public const string ENTRY_SCENE_NAME = "EntryScene";
    public const string HOME_SCENE_NAME = "HomeScene";
    public const string GAME_SCENE_NAME = "GameScene";
    public const string CREDIT_SCENE_NAME = "CreditScene";

    // Parameter
    public const float ROOT_GROW_PERFORM_TIME = 0.5f;
}
