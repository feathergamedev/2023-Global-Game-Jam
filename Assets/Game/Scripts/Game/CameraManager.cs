using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lapis.Extension;
using Cysharp.Threading.Tasks;
using System;

public class CameraManager : MonoBehaviour
{

    [SerializeField] CameraEffectController _cameraEffectController;


    [SerializeField] private float gameStartPosY;
    [SerializeField] private float gameStartPosMoveTime;
    [SerializeField] private Ease  gameStartMoveEaseType;

    [SerializeField] private RootTop rootTop;

    private float distanceBuffer;
    // Start is called before the first frame update
    void Start()
    {
        rootTop.OnPositionYChange += ChangeCameraPosY;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void ChangeCameraPosY(float posYChangeAmount)
    {
        transform.DOMoveY(transform.position.y + posYChangeAmount, 0.3f).SetEase(Ease.Linear);
    }

    public async UniTask ScrollToInitPos()
    {
        transform.DOLocalMoveY(gameStartPosY, gameStartPosMoveTime).SetEase(gameStartMoveEaseType);
        await UniTask.Delay(TimeSpan.FromSeconds(gameStartPosMoveTime + 0.5f));
    }

    public async UniTask EnterStageCameraPerform()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        transform.DOLocalMoveY(gameStartPosY, gameStartPosMoveTime).SetEase(gameStartMoveEaseType);

        await UniTask.Delay(TimeSpan.FromSeconds(gameStartPosMoveTime + 0.5f));
    }
}
