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

    void Awake()
    {
        slider = GetComponent<Slider>();

        var tracker = new ResourceTracker(
            time: new ResourceSetting<ulong>(0, 100, 100),
            energy: new ResourceSetting<uint>(0, 100, 100),
            water: new ResourceSetting<uint>(0, 100, 100),
            fertilizer: new ResourceSetting<uint>(0, 100, 100),
            branches: new ResourceSetting<uint>(0, 100, 100));


        tracker.ResourceValueChanged += (sender, args) =>
        {
            switch (args.NewValue)
            {
                case EnergyResource E:
                    // Update value to UI or somewhere
                    // args.Value;
                    if (args.Type == ResourceValueChangedEventArgs.ChangeType.Increase)
                    {
                        slider.value = E.Value;
                        // do something 
                    }
                    else if (args.Type == ResourceValueChangedEventArgs.ChangeType.Decrease)
                    {
                        slider.value -= E.Value;
                        // do something 
                    }
                    break;
                case TimeResource t:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            tracker.IncreaseEnergy(10);
            tracker.DecreaseEnergy(5);

        };

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
