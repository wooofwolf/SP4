using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float shootRadius = 5f;
    public bool ranged = false;
    public bool flying = false;
    public GameObject weapon;

    private Transform player;
    private NavMeshAgent agent;
    private MeleeWeapons sword;
    private RangedWeapon gun;
    private EnemyStats stats;
    private GameObject shield;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        stats = GetComponent<EnemyStats>();
        shield = transform.Find("ShieldParticles").gameObject;
        if (!flying)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        if (!ranged)
        {
            sword = weapon.GetComponent<MeleeWeapons>();
        }
        else
        {
            gun = weapon.GetComponent<RangedWeapon>();
        }

        if (stats.hasShield)
        {
            shield.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= lookRadius && !flying)
        {
            // Chase the player
            agent.SetDestination(player.position);

            if (!ranged)
            {
                if (distance <= agent.stoppingDistance)
                {
                    // Stop moving
                    agent.SetDestination(transform.position);
                    // Attack the player
                    sword.Attack();
                    // Face the player
                    FaceTarget();
                }
            }
            else
            {
                if (distance <= shootRadius)
                {
                    // Check line of sight
                    if (HasLineOfSight())
                    {
                        // Aim at player
                        gun.Aim(player);
                        // Stop moving
                        agent.SetDestination(transform.position);
                        // Shoot the player
                        gun.Attack(player);
                        // Face the player
                        FaceTarget();
                    }
                    else
                    {
                        gun.DeleteAim();
                    }
                }
                else
                {
                    gun.DeleteAim();
                }
            }
        }
        else if (flying && ranged)
        {
            if (distance <= shootRadius)
            {
                // Check line of sight
                if (HasLineOfSight())
                {
                    // Aim at player
                    gun.Aim(player);
                    // Shoot the player
                    gun.Attack(player);
                    // Face the player
                    FaceTarget();
                }
                else
                {
                    gun.DeleteAim();
                }
            }
            else
            {
                gun.DeleteAim();
            }
        }
    }

    bool HasLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }

        return false;
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void DisableShield()
    {
        shield.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        if (ranged)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, shootRadius);
        }
    }
}
