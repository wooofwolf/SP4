using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHazard : MonoBehaviour
{
    private CharacterStats myStats;
    private float attackCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(other.GetComponent<PlayerStats>(), attackCooldown));
            attackCooldown = myStats.AttackSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.Damage);
    }
}
