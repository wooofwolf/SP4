using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneHPNoDie : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>();
            other.GetComponent<PlayerStats>().invul = true;
            other.GetComponent<PlayerStats>().InvulnTimer = other.GetComponent<PlayerStats>().maxInvulnTimer;
            //timerbar.SetActive(true);
            Destroy(gameObject);
        }
    }


}
