using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanCollision : MonoBehaviour
{
    Rigidbody player_rb;
    public float fanPushForce;

    public bool colliding = false;

    // Start is called before the first frame update
    void Start()
    {
        //player_rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (colliding)
        {
            player_rb.AddForce(gameObject.transform.up * fanPushForce);

            float maxSpeed = 10f;
            player_rb.velocity = Vector3.ClampMagnitude(player_rb.velocity, maxSpeed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            player_rb = other.GetComponent<Rigidbody>();

            colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliding = false;
    }
}
