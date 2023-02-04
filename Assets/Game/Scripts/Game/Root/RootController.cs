using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lapis.Extension;
using System;
using Cysharp.Threading.Tasks;

public enum PlayerState
{
    SetDirection = 1,
    SetScale = 2,
    Growing = 3,
}

public class RootController : MonoBehaviour, IRootController
{
    public event Action<int> OnGrowAction; 
    public event Action OnRootCrash;
    public void StartGrow() { }
    public void StopGrow() { }

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

    [Header("Record")]
    private Transform lastRecordPoint;
    private int recordCounter;

    // Start is called before the first frame update
    void Start()
    {
        _currentScaleAmount = _minlengthScale;
        _lengthGrowDirection = 1;

        lastRecordPoint = rootTop.transform;
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

    public async UniTask SwitchBranch()
    {
        //TODO : 要換分支，目前先做成直接Game Over。
        Debug.Log("DIE!!!!!");
    }

    private void StateMachine()
    {
        switch (_currentPlayerState)
        {
            case PlayerState.SetDirection:
                var moveDirection = Input.GetAxisRaw("Horizontal");
                var newRotation = rootTop.transform.eulerAngles.z + (moveDirection * _rotateSpeed);
                rootTop.transform.rotation = Quaternion.Euler(new Vector3(0, 0, newRotation));
                //                rootTop.transform.SetRotationZ(moveDirection * _rotateSpeed);
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
                AudioManager.Instance.PlaySFX(ESoundEffectType.GrowRoot);
                recordCounter++;
                break;
        }
    }

    private IEnumerator GrowRootSequence()
    {
//        OnGrowAction?.Invoke();
        StretchRootTopTo(_nextGrowFinalNode.position);
        _currentPlayerState = PlayerState.Growing;
        _lengthIndicator.SetActive(false);

        yield return new WaitForSeconds(Config.ROOT_GROW_PERFORM_TIME);

        if (recordCounter == 3)
        {
            lastRecordPoint = rootTop.transform;
            Debug.Log("New record point.");
            recordCounter = 0;
        }    

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
