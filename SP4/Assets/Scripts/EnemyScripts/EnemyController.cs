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
    public float walkPointRange;
    public LayerMask whatIsGround;

    private Transform player;
    private NavMeshAgent agent;
    private MeleeWeapons sword;
    private RangedWeapon gun;
    private EnemyStats stats;
    private GameObject shield;
    private Vector3 walkPoint;
    private bool walkPointSet;
    private float originalY;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        stats = GetComponent<EnemyStats>();
        shield = transform.Find("ShieldEffects").gameObject;
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
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Bounce enemy
        if (flying)
        {
            float newY = Mathf.Sin(Time.time * 2f);
            transform.position = new Vector3(transform.position.x, newY + originalY, transform.position.z);
        }

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
            // Attack
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
        else
        {
            if (!walkPointSet)
            {
                SearchWalkPoint();
            }
            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
            }

            float distanceToWalkPoint = Vector3.Distance(walkPoint, transform.position);
            if (distanceToWalkPoint <= agent.stoppingDistance)
                walkPointSet = false;
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

        GameObject.Find("Player").GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0));
    }

    void SearchWalkPoint()
    {
        float x = Random.Range(-walkPointRange, walkPointRange);
        float z = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
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
