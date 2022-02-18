using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public bool damageTaken = false;

    private GameObject player;

    private void Awake()
    {
        Destroy(gameObject, 3f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !damageTaken)
        {
            // Damage the enemy
            damageTaken = true;
            CharacterStats targetStats = other.GetComponent<CharacterStats>();
            targetStats.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
