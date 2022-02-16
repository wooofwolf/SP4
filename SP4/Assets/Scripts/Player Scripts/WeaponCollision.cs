using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public WeaponController weapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && weapon.isAttacking)
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(1);
        }
    }
}
