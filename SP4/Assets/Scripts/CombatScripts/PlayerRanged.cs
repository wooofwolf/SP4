using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanged : MonoBehaviour
{
    public GameObject bullet;
    public Transform orientation;
    public float shootForce = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        //orientation = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootProj()
    {
        Debug.Log("killme");    
        GameObject newBullet = Instantiate(bullet, transform.position /*+ transform.forward*/, Quaternion.identity);

        newBullet.GetComponent<Rigidbody>().AddForce(orientation.forward * shootForce, ForceMode.Impulse);
    }
}
