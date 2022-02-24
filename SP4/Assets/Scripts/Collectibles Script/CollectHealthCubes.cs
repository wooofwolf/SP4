using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHealthCubes : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        //showhealed.Showhealed();
        if (other.CompareTag("Player"))
        {    
            //Debug.Log("show screen");
            FindObjectOfType<AudioManager>().Play("Collection");
            other.GetComponent<PlayerStats>().showheal = true;
            Destroy(gameObject);
        }
    }
    
}
