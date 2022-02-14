using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject inputMethod;
    public float doorSpeed = 2.0f;
    bool doorStatus;

    Vector3 closedPosition;
    Vector3 openPosition;

    float interpolationModifier;

    // Start is called before the first frame update
    void Start()
    {
        closedPosition = gameObject.transform.position;
        openPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        doorStatus = inputMethod.transform.Find("CollisionArea").GetComponent<SwitchCollision>().GetSwitchStatus();

        if (doorStatus)
        {
            if (interpolationModifier <= 1.0f)
            {
                interpolationModifier += Time.deltaTime * doorSpeed;
            }
        }
        else
        {
            if (interpolationModifier >= 0.0f)
            {
                interpolationModifier -= Time.deltaTime * doorSpeed;
            }
        }
        
        gameObject.transform.position = Vector3.Lerp(closedPosition, openPosition, interpolationModifier);
    }
}
