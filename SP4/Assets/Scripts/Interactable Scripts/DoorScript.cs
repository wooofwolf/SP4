using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject linkedSwitch;
    bool doorStatus;

    Vector3 closedPosition;
    Vector3 openPosition;

    // Start is called before the first frame update
    void Start()
    {

        closedPosition = gameObject.transform.position;
        openPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        doorStatus = linkedSwitch.transform.Find("CollisionArea").GetComponent<SwitchCollision>().GetSwitchStatus();

        if (doorStatus)
        {
            gameObject.transform.position = openPosition;
        }
        else
        {
            gameObject.transform.position = closedPosition;
        }

        //gameObject.transform.position = interpolatedPosition;

    }
}
