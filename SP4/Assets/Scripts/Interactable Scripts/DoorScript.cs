using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject inputMethod;
    public float doorSpeed = 4.0f;
    public AudioSource open, close;
    public float yOffset;
    bool doorStatus;

    Vector3 closedPosition;
    Vector3 openPosition;

    float interpolationModifier = -0.01f;

    // Start is called before the first frame update
    void Start()
    {
        closedPosition = gameObject.transform.position;
        openPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + yOffset, gameObject.transform.position.z);
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
                // Open door
                if (!open.isPlaying)
                {
                    open.Play();
                    close.Stop();
                }
            }
        }
        else
        {
            if (interpolationModifier >= 0.0f)
            {
                interpolationModifier -= Time.deltaTime * doorSpeed;
                // Close door
                if (!close.isPlaying)
                {
                    close.Play();
                    open.Stop();
                }
            }
        }
        
        gameObject.transform.position = Vector3.Lerp(closedPosition, openPosition, interpolationModifier);
    }
}
