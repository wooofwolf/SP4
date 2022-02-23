using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScoreSphere : MonoBehaviour
{
    bool collected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collected == false)
            {
                // Play audio clip
                FindObjectOfType<AudioManager>().Play("Collection");
                ScoreSystem.Score = ScoreSystem.Score + 20;
                //gameObject.SetActive(false);
                Destroy(gameObject);
                collected = true;
            }
        }
    }
}
