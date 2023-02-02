using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{

    [SerializeField] private Button enterGameButton;

    // Start is called before the first frame update
    void Start()
    {
        enterGameButton.onClick.AddListener(() => SceneTransitionManager.Instance.SwitchScene(SceneType.Game));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
