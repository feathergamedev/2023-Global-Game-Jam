using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lapis.Extension;

public enum PlayerState
{
    SetDirection = 1,
    SetScale = 2,
    Growing = 3,
}

public class RootController : MonoBehaviour
{

    private PlayerState _currentPlayerState = PlayerState.SetDirection;

    [SerializeField] private RootTop rootTop;
    [SerializeField] private Transform _nextGrowFinalNode;

    [Header("Scale")]
    [SerializeField] private GameObject _lengthIndicator;
    [SerializeField] private float _scaleChangeRate = 10f;
    [SerializeField] private float _lengthGrowDirection = 1;
    [SerializeField] private float _minlengthScale = 3f;
    [SerializeField] private float _maxlengthScale = 8f;
    private float _currentScaleAmount = 2f;

    [Header("Rotation")]
    [SerializeField] private float _rotateSpeed = 2;



    // Start is called before the first frame update
    void Start()
    {
        _currentScaleAmount = _minlengthScale;
        _lengthGrowDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Confirm"))
        {
            OnPlayerClick();
        }
    }

    private void StateMachine()
    {
        switch (_currentPlayerState)
        {
            case PlayerState.SetDirection:
                _lengthIndicator.transform.SetRotationZ(_rotateSpeed);
            break;

            case PlayerState.SetScale:
                _currentScaleAmount += _scaleChangeRate * _lengthGrowDirection * UnityEngine.Time.deltaTime;
                _lengthIndicator.transform.SetScaleY(_currentScaleAmount);
                if (_currentScaleAmount >= _maxlengthScale || _currentScaleAmount <= _minlengthScale)
                    _lengthGrowDirection *= -1;
            break;
        }
    }

    private void OnPlayerClick()
    {
        switch (_currentPlayerState)
        {
            case PlayerState.SetDirection:
                _currentPlayerState = PlayerState.SetScale;
                break;

            case PlayerState.SetScale:
                StartCoroutine(GrowRootSequence());
                break;
        }
    }

    private IEnumerator GrowRootSequence()
    {
        StretchRootTopTo(_nextGrowFinalNode.position);
        _currentPlayerState = PlayerState.Growing;
        _lengthIndicator.SetActive(false);

        yield return new WaitForSeconds(Config.ROOT_GROW_PERFORM_TIME);

        _lengthIndicator.SetActive(true);
        _currentPlayerState = PlayerState.SetDirection;

    }

    private void StretchRootTopTo(Vector3 position)
    {
        rootTop.MoveTo(position);

        _currentScaleAmount = _minlengthScale;
        _lengthGrowDirection = 1;
        _lengthIndicator.transform.SetScaleY(_currentScaleAmount);
    }
}
