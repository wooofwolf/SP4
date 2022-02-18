using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttkSpeedUp : MonoBehaviour
{

    Animator animator;
    public GameObject weapon;
    public float animSpeed = 3f;
    public float attkSpeedTimer = 10f;
    public bool attkUp = false;
    float maxTimer;
    // Start is called before the first frame update
    void Awake()
    {
        //weapon = GameObject.Find("Weapon");
        animator = weapon.GetComponent<Animator>();
        maxTimer = attkSpeedTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (attkUp)
        {
            attkSpeedTimer -= Time.deltaTime;
            //Debug.Log(attkSpeedTimer);
            if (attkSpeedTimer <= 0)
            {
                attkSpeedTimer = maxTimer;
                animator.speed = 1f;
                attkUp = false;
                Destroy(gameObject);
            }
        }
        //Debug.Log(animator.speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        animator.speed = animSpeed;
        attkUp = true;
        transform.position = new Vector3(0, 1000, 0);
        //Debug.Log("Speedup");
    }
}
