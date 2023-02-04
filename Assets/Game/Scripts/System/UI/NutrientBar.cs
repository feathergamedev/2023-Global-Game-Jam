using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutrientBar : MonoBehaviour
{
    public EnergyResource energy;
    public Image fillimgae;
    private Slider slider;
    private ResourceTracker _resourceTracker;

    public void Init(ResourceTracker ResourceTracker, GameSetting gameSetting)
    {
        _resourceTracker = ResourceTracker;

        _resourceTracker.ResourceValueChanged += (sender, args) =>
        {
            switch (args.NewValue)
            {
                case EnergyResource E:
                    // Update value to UI or somewhere
                    // args.Value;
                    if (args.Type == ResourceValueChangedEventArgs.ChangeType.Increase)
                    {
                        gameSetting.EnergyLimit = E.Value;
                        // do something 
                    }
                    else if (args.Type == ResourceValueChangedEventArgs.ChangeType.Decrease)
                    {
                        gameSetting.EnergyLimit = E.Value;
                        // do something 
                    }
                    break;
            }
        };

    }
    void Awake()
    {
        slider = GetComponent<Slider>();
    }


    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillimgae.enabled = false;
        }

        if (slider.value > slider.maxValue && !fillimgae.enabled)
        {
            fillimgae.enabled = true;
        }
        //float fillvalue = 
        //slider.value = fillvalue;
    }
}