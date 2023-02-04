using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class RootTop : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody2D;

    public event Action<float> OnPositionYChange;
    private float initPosY;
    private bool isMoving = false;

    [SerializeField] private LineRenderer _lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        initPosY = transform.position.y;

//        _lineRenderer.material.SetTextureScale("_MainTex", new Vector2(5f, 5f));

        _lineRenderer.positionCount = 2;
        var initLinePointPos = new Vector3(transform.position.x, transform.position.y, 0);
        _lineRenderer.SetPosition(0, initLinePointPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            var currentPointPos = new Vector3(transform.position.x, transform.position.y, 0);
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, currentPointPos);
        }
    }

    public void MoveTo(Vector3 position)
    {
        isMoving = true;

        if (position.y > initPosY)
            position.y = initPosY;

        var posYChangeAmount = position.y - transform.position.y;
        OnPositionYChange?.Invoke(posYChangeAmount);
        transform.DOMove(position, Config.ROOT_GROW_PERFORM_TIME).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _lineRenderer.positionCount++;
                var newLinePointPos = new Vector3(transform.position.x, transform.position.y, 0);
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newLinePointPos);
                isMoving = false;
            });
    }
}
