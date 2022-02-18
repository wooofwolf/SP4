using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVETYPE
{
    LOOP = 0,
    PINGPONG
}

public class MovingPlatform : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject[] points;
    public int pointNumber = 0;
    private Vector3 currentTarget;

    public float tolerance;
    public float speed;
    public float delayTime;

    private float delayStart;

    public MOVETYPE platformType;
    public bool automatic;
    private int platformDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (points.Length > 0)
        {
            currentTarget = points[0].transform.position;
        }

        tolerance = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position != currentTarget)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    public void MovePlatform()
    {
        Vector3 heading = currentTarget - transform.position;
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            transform.position = currentTarget;
            delayStart = Time.time;
        }
    }

    public void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - delayStart > delayTime)
            {
                if (platformType == MOVETYPE.LOOP)
                {
                    LoopPlatform();
                }
                else if (platformType == MOVETYPE.PINGPONG)
                {
                    PingpongPlatform();
                }
            }
        }
    }

    public void LoopPlatform()
    {
        pointNumber++;

        if (pointNumber >= points.Length)
        {
            pointNumber = 0;
        }
        currentTarget = points[pointNumber].transform.position;
    }

    public void PingpongPlatform()
    {
        pointNumber += platformDirection;

        if (pointNumber + 1 >= points.Length || pointNumber - 1 < 0)
        {
            platformDirection = -platformDirection;
        }
        currentTarget = points[pointNumber].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
