using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPanel : MonoBehaviour
{
    public Timeer Timeer;
    public Water Water;
    public NutrientBar NutrientBar;
    public CrisBar CrisBar;

    [SerializeField] private CanvasGroup GameplayUi;

    public void Init(ResourceTracker resourceTracker, GameSetting gameSetting)
    {
        GameplayUi.alpha = 0;
        Water.Init(resourceTracker, gameSetting);
        Timeer.Init(resourceTracker);
        NutrientBar.Init(resourceTracker, gameSetting);
        CrisBar.Init(resourceTracker, gameSetting);
    }

    public void ShowPanel()
    {  
        DOTween.To(() => GameplayUi.alpha, x => GameplayUi.alpha = x, 1f, 0.5f);
    }

    public void HidePanel()
    {
        DOTween.To(() => GameplayUi.alpha, x => GameplayUi.alpha = x, 0f, 0.5f); 
    }
}
