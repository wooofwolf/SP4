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
