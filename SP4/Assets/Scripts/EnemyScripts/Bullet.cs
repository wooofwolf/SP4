using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool damageTaken = false;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        Destroy(gameObject, 3f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !damageTaken)
        {
            // Damage player
            damageTaken = true;
            CharacterStats targetStats = player.GetComponent<CharacterStats>();
            targetStats.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
