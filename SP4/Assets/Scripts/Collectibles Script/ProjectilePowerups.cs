using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePowerups : MonoBehaviour
{
    public showProj ShowProj;
    PlayerMovement player;
    //public bool pickedUp = false;
    public float projTimer;
    public float maxProjTimer;
    public float projeffectTimer = 2.0f;
    public bool projeffectCheck = false;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        projTimer = maxProjTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.pickedUpProjPU)
        {
            projTimer -= Time.deltaTime;
            projeffectTimer -= Time.deltaTime;
            if (projTimer <= 0)
            {
                projTimer = maxProjTimer;
                player.pickedUpProjPU = false;
                Destroy(gameObject);
            }
            if (projeffectTimer <= 0)
            {
                projeffectCheck = false;
            }
        }

        if (projeffectCheck == true) showProj();
        else if (projeffectCheck == false) stopProj();

    }

    private void OnTriggerEnter(Collider other)
    {
        player.pickedUpProjPU = true;
        projeffectCheck = true;
        transform.position = new Vector3(0, 100, 0);
    }

    public void ShootProj()
    {

    }

    public void showProj()
    {
        ShowProj.showproj();
    }

    public void stopProj()
    {
        ShowProj.stopproj();
    }
}
