using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lapis.Extension;

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
        StartCoroutine(EnterStagePerform());

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

    private IEnumerator EnterStagePerform()
    {
        yield return new WaitForSeconds(1f);
        transform.DOLocalMoveY(gameStartPosY, gameStartPosMoveTime).SetEase(gameStartMoveEaseType);

        yield return new WaitForSeconds(gameStartPosMoveTime + 0.5f);
    }
}
