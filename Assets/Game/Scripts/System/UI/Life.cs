using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int life;

    [SerializeField] private List<Animator> _animators;

    private void Start()
    {

    }

    public void LoseLife(int index)
    {
        _animators[index - 1].SetBool("IsLose", true);
    }
}
