using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    public bool IsControlling;

    public void Update()
    {
        if (IsControlling)
        {
            var p = transform.localPosition;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                p.x += -2;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                p.x += 2;
            }
            transform.localPosition = p;
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter!!");
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter!!");
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D!!");
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D!!");
    }
}
