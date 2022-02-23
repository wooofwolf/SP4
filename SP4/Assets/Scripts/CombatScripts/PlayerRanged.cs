using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanged : MonoBehaviour
{
    public GameObject bullet;
    public Transform orientation;
    public float shootForce = 10f;
    public float reloadTime = 3f;
    float maxReloadTime;
    public bool canShoot = true;
    // Start is called before the first frame update
    void Awake()
    {
        maxReloadTime = reloadTime;
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
    }

    public void ShootProj()
    {
        if (canShoot)
        {
            GameObject newBullet = Instantiate(bullet, transform.position /*+ transform.forward*/, Quaternion.identity);
            GameObject newBullet1 = Instantiate(bullet, transform.position /*+ transform.forward*/, Quaternion.identity);
            GameObject newBullet2 = Instantiate(bullet, transform.position /*+ transform.forward*/, Quaternion.identity);

            newBullet.GetComponent<Rigidbody>().AddForce(orientation.forward * shootForce, ForceMode.Impulse);
            canShoot = false;
        }
    }
}
