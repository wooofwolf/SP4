using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleJump : MonoBehaviour
{
    private PlayerStats player;
    public float tripJumpTimer = 15f;
    private float maxTimer;
    public ShowTJ showTj;
    public float tjeffectTime = 3.0f;
    public bool tjeffectscheck = false;
    public GameObject tallJumpTimerBar;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
        maxTimer = tripJumpTimer;
    }
    private void Update()
    {
        if (player.tripJump)
        {
            TimerBar slider = tallJumpTimerBar.GetComponent<TimerBar>();
            slider.SetMaxBar(maxTimer);
            tripJumpTimer -= Time.deltaTime;
            slider.SetBar(tripJumpTimer);
            if (tripJumpTimer <= 0)
            {
                tallJumpTimerBar.SetActive(false);
                tripJumpTimer = maxTimer;
                player.tripJump = false;
            }
            if (tripJumpTimer <= maxTimer - tjeffectTime)
            {
                tjeffectscheck = false;
            }
        }

        if (tjeffectscheck == true)
        { 
            showTj.showtj(); 
        }
        else
        { 
            showTj.stoptj(); 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.tripJump = true;
            tjeffectscheck = true;
            tripJumpTimer = maxTimer;
            tallJumpTimerBar.SetActive(true);
            transform.position = new Vector3(0, 1000, 0);
            Destroy(gameObject, maxTimer);
        }
    }

    public void Showtj()
    {
        showTj.showtj();
    }

    public void Stoptj()
    {
        showTj.stoptj();
    }
}
