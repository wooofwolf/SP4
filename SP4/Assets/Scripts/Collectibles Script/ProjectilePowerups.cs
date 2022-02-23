using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePowerups : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerRanged>().pickedUpProjPU = true;
            other.GetComponent<PlayerRanged>().projeffectCheck = true;
            other.GetComponent<PlayerRanged>().projTimer = other.GetComponent<PlayerRanged>().maxProjTimer;
            Destroy(gameObject);
        }
    }

   
}
