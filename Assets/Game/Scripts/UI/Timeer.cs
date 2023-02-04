using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeer : MonoBehaviour
{
    public TimeResource time;
    public float timeValue = 90;
    public Text timerText;
    private ResourceTracker _resourceTracker;

    public void Init(ResourceTracker ResourceTracker)
    {
        _resourceTracker = ResourceTracker;

        _resourceTracker.ResourceValueChanged += (sender, args) =>
        {
            switch (args.NewValue)
            {
                case TimeResource T:
                    // Update value to UI or somewhere
                    // args.Value;
                    if (args.Type == ResourceValueChangedEventArgs.ChangeType.Increase)
                    {
                        timeValue = T.Value;
                        // do something 
                    }
                    if (args.Type == ResourceValueChangedEventArgs.ChangeType.Decrease)
                    {
                        timeValue = T.Value;
                        // do something 
                    }
                    break;
            }
        };
    }


    //Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
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

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}:", minutes, seconds);

    }
}