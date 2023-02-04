using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    public Water water;
    public Fillimgae fillimgae;
    private Slider slider;

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

        if (slider.value > slider.maxValue && !fillimgae.enbled)
        {
            fillimgae.enabled = true;
        }
        //float fillvalue = PlayerWater.current / playerWater.max;
        slider.value = fillvalue;
    }
}
