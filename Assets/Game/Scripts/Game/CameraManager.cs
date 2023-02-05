using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lapis.Extension;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public event Action<float> OnPositionYChanged;

    [SerializeField] CameraEffectController _cameraEffectController;


    [SerializeField] private float gameStartPosY;
    [SerializeField] private float gameStartPosMoveTime;
    [SerializeField] private Ease  gameStartMoveEaseType;

    [SerializeField] private float backToInitPosMoveTime;
    [SerializeField] private Ease backToInitPosMoveEaseType;

    [SerializeField] private RootTop rootTop;

    [SerializeField] private Text _gameOverText;

    private float distanceBuffer;
    // Start is called before the first frame update
    void Start()
    {
        rootTop.OnPositionYChange += yChangeAmount =>
        {
            ChangeCameraPosY(yChangeAmount);
            OnPositionYChanged?.Invoke(yChangeAmount);
        };
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
        transform.DOLocalMoveY(gameStartPosY, backToInitPosMoveTime).SetEase(backToInitPosMoveEaseType);
        await UniTask.Delay(TimeSpan.FromSeconds(backToInitPosMoveTime + 0.5f));
    }

    public async UniTask EnterStageCameraPerform()
    {
        transform.DOLocalMoveY(gameStartPosY, gameStartPosMoveTime).SetEase(gameStartMoveEaseType);

        await UniTask.Delay(TimeSpan.FromSeconds(gameStartPosMoveTime + 0.5f));
    }

    public async UniTask ShowGameOverText(string reasonString)
    {
        _gameOverText.text = reasonString;

        _gameOverText.transform.localPosition = new Vector3(-1500, 0, 0);
        _gameOverText.transform.DOLocalMove(new Vector3(0, 0, 0), 1).SetEase(Ease.OutBack);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        _gameOverText.transform.DOLocalMove(new Vector3(1500, 0, 0), 1).SetEase(Ease.InBack);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
    }
}
