using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectHealthCubes : MonoBehaviour
{
    public AudioSource collectionSound;
    private PlayerStats player;
    Healthbar healthbar;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    private void OnTriggerEnter(Collider other)
    {
        collectionSound.Play();
        player.CurrentHealth += 1;
        player.Heal(1);
        Destroy(gameObject);
        Debug.Log("Health+1");
    }
}
