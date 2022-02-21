using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleJump : MonoBehaviour
{
    private PlayerStats player;
    public float tripJumpTimer = 15f;
    private float maxTimer;
    public ShowTJ showTj;
    public float tjeffectTime = 2.0f;
    public bool tjeffectscheck = false;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
        maxTimer = tripJumpTimer;
    }
    private void Update()
    {
        if (player.tripJump)
        {
            tripJumpTimer -= Time.deltaTime;
            tjeffectTime -= Time.deltaTime;

            // Debug.Log(tripJumpTimer);
            if (tripJumpTimer <= 0)
            {
                tripJumpTimer = maxTimer;
                player.tripJump = false;
                Destroy(gameObject);
            }
            if (tjeffectTime <= 0)
            {
                tjeffectscheck = false;
            }
        }

        if (tjeffectscheck == true) Showtj();
        else if (tjeffectscheck == false) Stoptj();
    }
    private void OnTriggerEnter(Collider other)
    {
        player.tripJump = true;
        tjeffectscheck = true;
        transform.position = new Vector3(0, 1000, 0);
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
