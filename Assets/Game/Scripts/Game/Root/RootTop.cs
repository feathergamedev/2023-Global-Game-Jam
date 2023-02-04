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


    // Start is called before the first frame update
    void Start()
    {
        initPosY = transform.position.y;
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
        transform.DOMove(position, Config.ROOT_GROW_PERFORM_TIME).SetEase(Ease.Linear);
    }
}
