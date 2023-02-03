using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Button homeButton;
    [SerializeField] private Button creditButton;

    // Start is called before the first frame update
    void Start()
    {
        homeButton.onClick.AddListener(() => SceneTransitionManager.Instance.SwitchScene(SceneType.Home));
        creditButton.onClick.AddListener(() => SceneTransitionManager.Instance.SwitchScene(SceneType.Credit));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
