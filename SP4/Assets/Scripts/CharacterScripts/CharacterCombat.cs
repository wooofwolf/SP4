using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private CharacterStats myStats;
    private float attackCooldown = 0f;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            Debug.Log("Attacking...");
            targetStats.TakeDamage(myStats.Damage);
            attackCooldown = myStats.AttackSpeed;
        }
    }
}
