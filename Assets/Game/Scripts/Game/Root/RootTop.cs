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

    [SerializeField] private LineRenderer _lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        initPosY = transform.position.y;

        _lineRenderer.positionCount = 1;
        var initLinePointPos = new Vector3(transform.position.x, transform.position.y, 0);
        _lineRenderer.SetPosition(0, initLinePointPos);
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void MoveTo(Vector3 position)
    {
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
            });

    }
}
