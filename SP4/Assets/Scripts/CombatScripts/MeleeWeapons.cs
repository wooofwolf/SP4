using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapons : MonoBehaviour
{
    public bool canAttack = true;
    public bool damageTaken = false;
    public bool isAttacking = false;
    public GameObject owner;

    private Animator animator;
    private CharacterStats stats;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        stats = owner.GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((other.tag == "Enemy" && owner.tag == "Player") || (other.tag == "Player" && owner.tag == "Enemy")) && !damageTaken && isAttacking)
        {
            CharacterStats stats = other.GetComponent<CharacterStats>();
            if (!stats.invul || (stats.invul && stats.CurrentHealth > 1))
            {
                stats.TakeDamage(stats.Damage);
            }
            else if (stats.invul && stats.CurrentHealth == 1)
            {
                stats.TakeDamage(0);
            }
            damageTaken = true;
        }
    }

    public void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        canAttack = false;
        isAttacking = true;
        animator.SetTrigger("Attack");
        // Play audio clip
        FindObjectOfType<AudioManager>().Play("Sword");

        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(stats.AttackSpeed);
        canAttack = true;
        damageTaken = false;
        isAttacking = false;
    }
}
