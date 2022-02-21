using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHPNoDie : MonoBehaviour
{
    public ShowInvuln showinvuln;
    public float InvulnTimer = 3.0f;
    private float maxInvulnTimer;
    //bool pickedup = false;
    private CharacterStats player;
    public float invulneffectTimer = 2.0f;
    public bool invulneffectcheck = false;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<CharacterStats>();
        maxInvulnTimer = InvulnTimer;
    }

    private void Update()
    {
        if (player.invul)
        {
            InvulnTimer -= Time.deltaTime;
            invulneffectTimer -= Time.deltaTime;
            //Debug.Log(InvulnTimer);
           if (InvulnTimer <= 0)
            {
                InvulnTimer = maxInvulnTimer;
                player.invul = false;
                Destroy(gameObject);
            }
           if (invulneffectTimer <= 0)
            {
                invulneffectcheck = false;
            }
        }

        if (invulneffectcheck == true) ShowInv();
        else if (invulneffectcheck == false) StopInv();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.invul = true;
        invulneffectcheck = true;
        //Debug.Log("Invulnerable");
        transform.position = new Vector3(0, -10, 0);
        //Invoke(gameobject.Destroy(), 3f);
        //Destroy(gameObject);
    }

    public void ShowInv()
    {
        showinvuln.Showinvuln();
    }

    public void StopInv()
    {
        showinvuln.Stopinvuln();
    }
}
