using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplingPoint;
    public LayerMask whatIsGrapple, whatIsGrapple2;
    public Transform gunTip, camera, player;
    private float maxDistance = 100;
    private SpringJoint joint;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();  
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
    }

    void StartGrapple()
    {
        RaycastHit hit;
        //shoot out a ray from camera
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrapple) || Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrapple2))
        {
            grapplingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplingPoint;

            float distanceFromGP = Vector3.Distance(player.position, grapplingPoint);

            //distance the grapple line try to be from the grapple point
            joint.maxDistance = distanceFromGP * 0.0f;
            joint.minDistance = distanceFromGP * 0.1f;


            //values for the grappling force
            joint.spring = 5.5f;
            joint.damper = 10f;
            joint.massScale = 1f;

            lr.positionCount = 2;

            // Play audio clip
            FindObjectOfType<AudioManager>().Play("GrapplingGun");
        }
    }

    //Called after Update
    private void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        //if not grappling, dont draw the rope
        if (!joint) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1 , grapplingPoint);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool isGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplingPoint;
    }
}
