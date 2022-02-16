using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public bool canAttack = true;
    public bool damageTaken = false;
    public GameObject owner;
    public GameObject bullet;
    public float shootForce;
    public float spread;

    private CharacterStats stats;

    private void Awake()
    {
        stats = owner.GetComponent<CharacterStats>();
    }

    public void Attack(Transform target)
    {
        if (!canAttack)
        {
            return;
        }

        Ray ray = new Ray(transform.position, target.position - transform.position);
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 direction = targetPoint - transform.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = direction + new Vector3(x, y, 0);

        // Instantiate bullet
        GameObject newBullet = Instantiate(bullet, transform.position + transform.forward, Quaternion.identity);
        newBullet.transform.forward = directionWithSpread.normalized;

        newBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

        canAttack = false;
        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(stats.AttackSpeed);
        canAttack = true;
        damageTaken = false;
    }
}
