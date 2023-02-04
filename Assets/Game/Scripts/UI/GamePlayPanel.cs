using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPanel : MonoBehaviour
{
    public Timeer Timeer;
    public Water Water;
    public NutrientBar NutrientBar;
    public CrisBar CrisBar;
    public void Init(ResourceTracker resourceTracker)
    {
        Water.Init(resourceTracker);
        Timeer.Init(resourceTracker);
        NutrientBar.Init(resourceTracker);
        CrisBar.Init(resourceTracker);
    }

    public void ShowPanel() { }

    public void HidePanel() { }
}
