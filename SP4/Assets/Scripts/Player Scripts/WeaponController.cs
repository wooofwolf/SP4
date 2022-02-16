using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject player;

    public GameObject weapon;
    public bool canSwing = true;
    public bool isAttacking = false;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canSwing)
            {
                SwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        isAttacking = true;
        canSwing = false;
        Animator animator = weapon.GetComponent<Animator>();
        animator.SetTrigger("Attack");

        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        yield return new WaitForSeconds(playerStats.AttackSpeed);

        isAttacking = false;
        canSwing = true;
    }
}
