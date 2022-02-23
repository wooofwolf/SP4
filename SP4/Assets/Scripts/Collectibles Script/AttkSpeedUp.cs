using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttkSpeedUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<AttkSpeedingUp>().attkUp = true;
            other.GetComponent<AttkSpeedingUp>().attkSpeedTimer = other.GetComponent<AttkSpeedingUp>().maxTimer;
            //speedBuff.ASeffectCheck = true;
            //speedBuff.timerbar.SetActive(true);
            Destroy(gameObject);
            //Debug.Log("Speedup");
        }
    }

}
