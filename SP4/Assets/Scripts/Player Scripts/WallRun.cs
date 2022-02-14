using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    public LayerMask WhatISWall;
    public float wallrunForce, maxWallruntime, maxWallSpeed;
    bool isWallRight, isWallLeft;
    bool isWallRunning;
    public float maxWallRunCameraTilt, wallRunCameraTilt;

    public Transform playerCam;
    public Transform orientation;

    private Rigidbody rb;

    private void WallRunInput()
    {

    }

    private void StartWallRun()
    {

    }

    private void StopWallRun()
    {

    }

    private void CheckForWall()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForWall();
        WallRunInput();
    }
}
