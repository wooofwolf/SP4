using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private CharacterStats myStats;
    private EnemyShooting myProjectile;
    private float attackCooldown = 0f;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        myProjectile = GetComponent<EnemyShooting>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void MeleeAttack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, myStats.AttackDelay));
            attackCooldown = myStats.AttackSpeed;
        }
    }

    public void RangedAttack()
    {
        if (attackCooldown <= 0f)
        {
            myProjectile.Shoot();
            attackCooldown = myStats.AttackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.Damage);
    }
}
