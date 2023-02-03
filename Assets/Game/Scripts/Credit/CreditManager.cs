using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{

    [SerializeField] private float _performTime;
    [SerializeField] private float _transitionDelayTime;
    [SerializeField] private CameraEffectController _cameraEffectController;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PerformEndSequence());
    }

    private IEnumerator PerformEndSequence()
    {
        yield return new WaitForSeconds(_performTime + _transitionDelayTime);

        _cameraEffectController.StartFadeOut(3);
        yield return new WaitForSeconds(4);

        SceneTransitionManager.Instance.SwitchScene(SceneType.Home);

    }
}
