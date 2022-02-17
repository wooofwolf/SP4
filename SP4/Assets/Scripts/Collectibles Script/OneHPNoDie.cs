using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHPNoDie : MonoBehaviour
{
    public float InvulnTimer = 3.0f;
    private float maxInvulnTimer;
    //bool pickedup = false;
    private CharacterStats player;
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
            //Debug.Log(InvulnTimer);
           if(InvulnTimer <= 0)
            {
                InvulnTimer = maxInvulnTimer;
                player.invul = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player.invul = true;
        Debug.Log("Invulnerable");
        transform.position = new Vector3(0, 1000, 0);
        //Invoke(gameobject.Destroy(), 3f);
        //Destroy(gameObject);
    }
}
