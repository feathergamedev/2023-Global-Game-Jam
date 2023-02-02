using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{

    [SerializeField] private float _performTime;
    [SerializeField] private float _transitionDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnterHomeScene", _performTime + _transitionDelayTime);
    }

    public void EnterHomeScene()
    {
        SceneTransitionManager.Instance.SwitchScene(SceneType.Home);
    }
}
