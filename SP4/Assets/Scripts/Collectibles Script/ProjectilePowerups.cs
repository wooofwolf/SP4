using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePowerups : MonoBehaviour
{
    PlayerMovement player;
    //public bool pickedUp = false;
    public float projTimer;
    public float maxProjTimer;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        projTimer = maxProjTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.pickedUpProjPU)
        {
            projTimer -= Time.deltaTime;
            if(projTimer <=0)
            {
                projTimer = maxProjTimer;
                player.pickedUpProjPU = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player.pickedUpProjPU = true;
        transform.position = new Vector3(0, 100, 0);
    }

    public void ShootProj()
    {
        
    }
}
