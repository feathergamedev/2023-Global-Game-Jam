using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class TreeGirl : MonoBehaviour
{

    [SerializeField] SpriteRenderer _mainSprite;
    [SerializeField] SpriteRenderer _coverSprite;

    [Header("Sprite")]
    [SerializeField] private List<Sprite> tierSpriteList;
    [SerializeField] private Sprite badEndSprite;

    public async UniTask SetFinalAppearance(TierComputer.Tier tier)
    {

        await Evolve(TierComputer.Tier.S);
        await UniTask.Delay(System.TimeSpan.FromSeconds(1));
    }

    private async UniTask Evolve(TierComputer.Tier tier)
    {
        float fadeDuration = 2f;

//        _mainSprite.sprite = tierSpriteList[0];
//        _coverSprite.sprite = tierSpriteList[3];

        _mainSprite.color = new Color32(255, 255, 255, 255);
        _coverSprite.color = new Color32(255, 255, 255, 0);

        DOTween.To(() => _mainSprite.color, x => _mainSprite.color = x, new Color32(255, 255, 255, 0), fadeDuration);
        DOTween.To(() => _coverSprite.color, x => _coverSprite.color = x, new Color32(255, 255, 255, 255), fadeDuration);

        await UniTask.Delay(System.TimeSpan.FromSeconds(fadeDuration));

        /*
        float fadeDuration = 1f;
        int listIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            _mainSprite.sprite = tierSpriteList[i];
            _coverSprite.sprite = tierSpriteList[i+1];

            _mainSprite.color = new Color32(255, 255, 255, 255);
            _coverSprite.color = new Color32(255, 255, 255, 0);

            _mainSprite.DOFade(0, fadeDuration);
            _coverSprite.DOFade(255, fadeDuration);

            await UniTask.NextFrame();

        }
        */

    }
}