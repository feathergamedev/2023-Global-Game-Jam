using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time : MonoBehaviour
{
    public float timeValue = 300;
    public Text timeText;

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
//            timeValue -=
        }
        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
