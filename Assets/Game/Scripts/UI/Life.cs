using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public GameObject[] roots;
    public int life;
    void Update()
    {
     if (life < 1)
        {
            Destroy(roots[0].gameObject);
        }else if (life < 2)
        {
            Destroy(roots[1].gameObject);
        }else if (life < 3)
        {
            Destroy(roots[3].gameObject);
        }
    }

    public void TakeDamage(int d)
    {
        life -= d;  
    }
}
