using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScoreSphere : MonoBehaviour
{
    public AudioSource collectionSound;
    bool collected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (collected == false)
            {
                collectionSound.Play();
                ScoreSystem.Score = ScoreSystem.Score + 20;
                //gameObject.SetActive(false);
                Destroy(gameObject);
                collected = true;
            }
        }
    }
}
