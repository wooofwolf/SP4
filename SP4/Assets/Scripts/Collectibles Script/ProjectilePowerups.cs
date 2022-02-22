using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePowerups : MonoBehaviour
{
    public showProj ShowProj;
    PlayerMovement player;
    public float projTimer;
    public float maxProjTimer;
    public float projeffectTimer = 2.0f;
    public bool projeffectCheck = false;
    public GameObject projTimerBar;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        projTimer = maxProjTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.pickedUpProjPU)
        {
            TimerBar slider = projTimerBar.GetComponent<TimerBar>();
            slider.SetMaxBar(maxProjTimer);
            projTimer -= Time.deltaTime;
            slider.SetBar(projTimer);
            if (projTimer <= 0)
            {
                projTimerBar.SetActive(false);
                projTimer = maxProjTimer;
                player.pickedUpProjPU = false;
            }
            if (projTimer <= maxProjTimer - projeffectTimer)
            {
                projeffectCheck = false;
            }
        }

        if (projeffectCheck == true)
        {
            ShowProj.showproj();
        }
        else if (projeffectCheck == false)
        {
            ShowProj.stopproj();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.pickedUpProjPU = true;
            projeffectCheck = true;
            projTimer = maxProjTimer;
            projTimerBar.SetActive(true);
            transform.position = new Vector3(0, 100, 0);
            Destroy(gameObject, maxProjTimer);
        }
    }

    public void showProj()
    {
        ShowProj.showproj();
    }

    public void stopProj()
    {
        ShowProj.stopproj();
    }
}
