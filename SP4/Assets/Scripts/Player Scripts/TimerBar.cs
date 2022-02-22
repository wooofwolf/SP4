using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxBar(float fill)
    {
        slider.maxValue = fill;
        slider.value = fill;
    }
    public void SetBar(float fill)
    {
        slider.value = fill;
    }
}
