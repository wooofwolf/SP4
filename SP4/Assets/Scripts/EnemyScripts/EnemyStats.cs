using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public bool hasShield = false;

    private void Awake()
    {
        MaxHealth = 1;
        CurrentHealth = MaxHealth;
    }

    public override void TakeDamage(int damage)
    {
        if (hasShield)
        {
            hasShield = false;
            gameObject.GetComponent<EnemyController>().DisableShield();

            GameObject player = GameObject.Find("Player");
            Transform orientation = player.transform.Find("Orientation");

            // Play audio clip
            FindObjectOfType<AudioManager>().Play("ShieldBreak");

            player.GetComponent<Rigidbody>().AddForce(new Vector3(-orientation.forward.x * 1000, 500, -orientation.forward.z * 1000));
        }
        else
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
