using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPanel : MonoBehaviour
{
    public Timeer Timeer;
    public Water Water;
    public NutrientBar NutrientBar;
    public CrisBar CrisBar;
    public void Init(ResourceTracker resourceTracker, GameSetting gameSetting)
    {
        Water.Init(resourceTracker, gameSetting);
        Timeer.Init(resourceTracker);
        NutrientBar.Init(resourceTracker, gameSetting);
        CrisBar.Init(resourceTracker, gameSetting);
    }

    public void ShowPanel() { }

    public void HidePanel() { }
}
