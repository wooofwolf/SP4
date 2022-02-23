using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleJump : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().tallJump = true;
            other.GetComponent<PlayerStats>().tallJumpTimer = other.GetComponent<PlayerStats>().maxTallJumpTimer;
            //playerstats.tallJumpTimerBar.SetActive(true);
            //transform.position = new Vector3(0, 1000, 0);
            Destroy(gameObject);
        }
    }
}
