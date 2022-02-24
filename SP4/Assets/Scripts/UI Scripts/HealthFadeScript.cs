using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFadeScript : MonoBehaviour
{
    public Image image;
    Color color;

    public float speed;
    public float pulseDelay;
    public float repeatingDelay;

    private bool fadeIn = false;
    private bool fadeOut = false;

    private void Start()
    {
        color = image.color;
    }

    public void ShowEffect()
    {
        fadeIn = true;
        //test.color = new Color(test.color.r, test.color.g, test.color.b, 1f);
    }

    public void HideEffect()
    {
        fadeOut = true;
        //test.color = new Color(test.color.r, test.color.g, test.color.b, 0f);
    }

    public void PulseEffectOnce()
    {
        Invoke("ShowEffect", 0f);
        Invoke("HideEffect", pulseDelay);
    }

    public void PulseEffectContinuous()
    {
        InvokeRepeating("PulseEffectOnce", 0.2f, repeatingDelay);
    }

    public void DisableOnePulse()
    {
        CancelInvoke("PulseEffectOnce");
    }

    public void DisableContinuousPulse()
    {
        CancelInvoke("PulseEffectOnce");
        CancelInvoke("PulseEffectContinuous");
    }

    // Update is called once per frame
    void Update()
    {

        if (fadeIn)
        {
            if (image.color.a < 1)
            {
                color.a += speed * Time.deltaTime;

                if (color.a >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        
        if (fadeOut)
        {
            if (image.color.a >= 0)
            {
                color.a -= speed * Time.deltaTime;

                if (color.a <= 0)
                {
                    fadeOut = false;
                }
            }
        }

        image.color = color;
    }
}
