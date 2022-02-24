using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashFadeScript : MonoBehaviour
{
    public Text test;
    Color color;

    public float speed = 10;

    private bool fadeIn = false;
    private bool fadeOut = false;

    private void Start()
    {
        color = test.color;
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
        Invoke("HideEffect", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (fadeIn)
        {
            if (test.color.a < 1)
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
            if (test.color.a >= 0)
            {
                color.a -= speed * Time.deltaTime;

                if (color.a <= 0)
                {
                    fadeOut = false;
                }
            }
        }

        test.color = color;
    }
}
