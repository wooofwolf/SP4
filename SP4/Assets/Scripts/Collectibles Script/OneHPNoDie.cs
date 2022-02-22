using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneHPNoDie : MonoBehaviour
{
    public ShowInvuln showinvuln;
    public float InvulnTimer = 3.0f;
    private float maxInvulnTimer;
    private CharacterStats player;
    public float invulneffectTimer = 3.0f;
    public bool invulneffectcheck = false;
    public GameObject timerbar;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<CharacterStats>();
        maxInvulnTimer = InvulnTimer;
    }

    private void Update()
    {
        if (player.invul)
        {
            TimerBar slider = timerbar.GetComponent<TimerBar>();
            slider.SetMaxBar(maxInvulnTimer);
            InvulnTimer -= Time.deltaTime;
            slider.SetBar(InvulnTimer);
           if (InvulnTimer <= 0)
           {
               timerbar.SetActive(false);
               InvulnTimer = maxInvulnTimer;
               player.invul = false;
           }
           if (InvulnTimer <= maxInvulnTimer - invulneffectTimer)
           {
               invulneffectcheck = false;
           }
        }

        if (invulneffectcheck == true)
        {
            showinvuln.Showinvuln();
        }
        else if (invulneffectcheck == false)
        {
            showinvuln.Stopinvuln();
        }
     }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.invul = true;
            invulneffectcheck = true;
            InvulnTimer = maxInvulnTimer;
            timerbar.SetActive(true);
            transform.position = new Vector3(0, 100, 0);
            Destroy(gameObject , maxInvulnTimer);
        }
    }

    public void ShowInv()
    {
        showinvuln.Showinvuln();
    }

    public void StopInv()
    {
        showinvuln.Stopinvuln();
    }
}
