using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        Destroy(gameObject, 3f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // Damage player
            CharacterStats targetStats = player.GetComponent<CharacterStats>();
            targetStats.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
