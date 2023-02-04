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
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator EnterStagePerform()
    {
        yield return new WaitForSeconds(1f);
        transform.DOLocalMoveY(gameStartPosY, gameStartPosMoveTime).SetEase(gameStartMoveEaseType);

        yield return new WaitForSeconds(gameStartPosMoveTime + 0.5f);
    }
}
