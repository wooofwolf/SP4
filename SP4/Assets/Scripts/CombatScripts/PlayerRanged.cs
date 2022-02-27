using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanged : MonoBehaviour
{
    public showProj ShowProj;
    PlayerMovement player;
    public float projTimer;
    public float maxProjTimer;
    public float projeffectTimer = 2.0f;
    public bool projeffectCheck = false;
    public GameObject projTimerBar;
    public bool pickedUpProjPU = false;

    public GameObject bullet;
    public float shootForce = 10f;
    public float reloadTime = 1f;
    float maxReloadTime;
    public bool canShoot = true;
    // Start is called before the first frame update
    void Awake()
    {
        maxReloadTime = reloadTime;
        maxProjTimer = projTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0)
            {
                canShoot = true;
                reloadTime = maxReloadTime;
            }
        }
        if (pickedUpProjPU)
        {
            projeffectCheck = true;
            projTimerBar.SetActive(true);
            TimerBar slider = projTimerBar.GetComponent<TimerBar>();
            slider.SetMaxBar(maxProjTimer);
            projTimer -= Time.deltaTime;
            slider.SetBar(projTimer);
            if (projTimer <= 0)
            {
                projTimerBar.SetActive(false);
                //projTimer = maxProjTimer;
                pickedUpProjPU = false;
            }
            if (projTimer <= maxProjTimer - projeffectTimer)
            {
                projeffectCheck = false;
            }
        }

        if (projeffectCheck == true)
        {
            ShowProj.showproj();
        }
        else if (projeffectCheck == false)
        {
            ShowProj.stopproj();
        }
    }

    public void ShootProj()
    {
        if (canShoot)
        {
            spawnBullet();
            Invoke( "spawnBullet" , 0.1f );

            canShoot = false;
        }
    }

    void spawnBullet()
    {
        GameObject bullets = Instantiate(bullet, transform.position, Quaternion.identity);
        bullets.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * shootForce, ForceMode.Impulse);
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
