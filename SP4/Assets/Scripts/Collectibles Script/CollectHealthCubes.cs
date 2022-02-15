using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHealthCubes : MonoBehaviour
{
    public AudioSource collectionSound;
    public PlayerStats player;
    Healthbar healthbar;
    private void OnTriggerEnter(Collider other)
    {
        collectionSound.Play();
        player.CurrentHealth += 1;
        player.Heal(1);
        Destroy(gameObject);
        Debug.Log("Health+1");
    }
}
