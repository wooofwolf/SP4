using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttkSpeedUp : MonoBehaviour
{
    public ShowAS Showas; 
    Animator animator;
    public GameObject weapon;
    public float animSpeed = 3f;
    public float attkSpeedTimer = 10f;
    public bool attkUp = false;
    float maxTimer;
    public float ASeffectTimer = 2.0f;
    public bool ASeffectCheck = false;
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
            ASeffectTimer -= Time.deltaTime;
            //Debug.Log(attkSpeedTimer);
            if (attkSpeedTimer <= 0)
            {
                attkSpeedTimer = maxTimer;
                animator.speed = 1f;
                attkUp = false;
                Destroy(gameObject);
            }
            if (ASeffectTimer <= 0)
            {
                ASeffectCheck = false;
            }
        }
        //Debug.Log(animator.speed);
        if (ASeffectCheck == true) showas();
        else if (ASeffectCheck == false) stopas();
    }
    private void OnTriggerEnter(Collider other)
    {
        animator.speed = animSpeed;
        attkUp = true;
        ASeffectCheck = true;
        transform.position = new Vector3(0, -10, 0);
        //Debug.Log("Speedup");
    }

    public void showas()
    {
        Showas.showAS();
    }

    public void stopas()
    {
        Showas.stopAS();
    }
}
