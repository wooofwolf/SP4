using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleJump : MonoBehaviour
{
    private PlayerStats player;
    public float tripJumpTimer = 15f;
    private float maxTimer;
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
           // Debug.Log(tripJumpTimer);
            if (tripJumpTimer <= 0)
            {
                tripJumpTimer = maxTimer;
                player.tripJump = false;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        player.tripJump = true;
        transform.position = new Vector3(0, 1000, 0);
    }
}
