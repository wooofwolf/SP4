using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttkSpeedingUp : MonoBehaviour
{
    
    bool ASeffectCheck = false;
    Animator animator;
    
    public ShowAS Showas;
    public GameObject weapon;
    public float animSpeed = 3f;
    public float attkSpeedTimer = 10f;
    public float maxTimer;
    public bool attkUp = false;
    public float ASeffectTimer = 3.0f;
    public GameObject timerbar;
    void Awake()
    {
        //weapon = GameObject.Find("Weapon");
        animator = weapon.GetComponent<Animator>();
        maxTimer = attkSpeedTimer;
    }
    // Update is called once per frame
    void Update()
    {
        if (attkUp)
        {
            ASeffectCheck = true;
            timerbar.SetActive(true);

            animator.speed = animSpeed;

            TimerBar slider = timerbar.GetComponent<TimerBar>();
            slider.SetMaxBar(maxTimer);

            attkSpeedTimer -= Time.deltaTime;
            slider.SetBar(attkSpeedTimer);

            //Debug.Log(attkSpeedTimer);
            if (attkSpeedTimer <= 0)
            {
                //attkSpeedTimer = maxTimer;
                animator.speed = 1f;
                attkUp = false;
                timerbar.SetActive(false);
            }
            if (attkSpeedTimer <= (maxTimer - ASeffectTimer))
            {
                ASeffectCheck = false;
            }
        }
        //Debug.Log(animator.speed);
        if (ASeffectCheck == true) showas();
        else if (ASeffectCheck == false) stopas();
    }
    public void showas()
    {
        Showas.showAS();
    }

    public void stopas()
    {
        Showas.stopAS();
    }
}
