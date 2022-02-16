// Some stupid rigidbody based movement by Dani

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public Transform playerCam;
    public Transform orientation;
    private Rigidbody rb;

    private float xRotation;
    private float sensitivity = 100f;
    private float sensMultiplier = 1f;
    public float moveSpeed = 3000;
    public float crouchMoveSpeed = 700;
    public float maxSpeed = 20;
    public bool grounded;
    public LayerMask whatIsGround;

    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 35f;

    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    public float slideForce = 400;
    public float slideCounterMovement = 0.1f;

    private bool readyToJump = true;
    private bool canDoubleJump = false;
    public float jumpForce = 550f;

    private bool readyToDash = true;
    public float dashCooldown = 1.0f;
    public float dashCDUI = 1.0f;
    public float dashForce = 550f;

    float x, y;
    bool jumping, sprinting, crouching, dashing;

    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    private int maxDoubleJumps;
    private int noOfDoubleJumpsLeft;

    public float fallMultiplier = 2.5f;
    public float lowJumpMulitplier = 2f;

    //wallrun stuff
    public LayerMask whatIsWall;
    public float wallrunForce, maxWallrunTime, maxWallrunSpeed;
    bool isWallRight, isWallLeft;
    bool isWallRunning;
    public float maxWallRunCameraTilt, wallRunCameraTilt;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        maxDoubleJumps = GetComponent<PlayerStats>().DoubleJumps;
        noOfDoubleJumpsLeft = maxDoubleJumps;
    }

    void Start()
    {
        playerScale = transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        Movement();
        WallRunInput();
        CheckForWall();
    }

    private void Update()
    {
        MyInput();
        Look();
        if (!readyToDash && dashCDUI <= 1.0f)
        {
            dashCDUI += Time.deltaTime;
        }
    

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMulitplier - 1) * Time.deltaTime;
        }
    }

    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        crouching = Input.GetKey(KeyCode.LeftControl);
        dashing = Input.GetKey(KeyCode.LeftShift);

        //Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void StartCrouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.5f)
        {
            if (grounded)
            {
                rb.AddForce(orientation.transform.forward * slideForce);

            }
        }
    }

    private void StopCrouch()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement()
    {
        //gravity
        rb.AddForce(Vector3.down * Time.deltaTime * 10);

        //velocity 2 whr player looking
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;

        //Counteract sliding 
        CounterMovement(x, y, mag);

        //dashing
        if (dashing && readyToDash) Dash();

        //Set max speed
        float maxSpeed = this.maxSpeed;

        //sliding down ramp
        if (crouching && grounded)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }

        //max speed cap
        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        float multiplier = 1f, multiplierV = 1f;

        // Movement in air aka strafing
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }

        // moving while sliding
        if (grounded && crouching) multiplierV = 0f;

        //applying force to move player
        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);
    }

    private void Jump()
    {
        if (grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce);
            canDoubleJump = true;
            noOfDoubleJumpsLeft = maxDoubleJumps;
        }
        else if (!isWallRunning && !grounded)
        {
            if (canDoubleJump)
            {
                noOfDoubleJumpsLeft -= 1;
                if (noOfDoubleJumpsLeft <= 0)
                {
                    canDoubleJump = false;
                }
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce);
            }
        }

        if (isWallRunning)
        {
            readyToJump = false;
            
            //normal jump since player not pressing down directional keys toward wall
            if (isWallLeft && !Input.GetKey(KeyCode.D) || isWallRight && !Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce);
                canDoubleJump = true;
                noOfDoubleJumpsLeft = maxDoubleJumps;
            }

            //sidewards wallhopping
            if (isWallRight || isWallLeft && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) rb.AddForce(-orientation.up * jumpForce * 1f);
            if (isWallRight && Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * jumpForce * 3.2f); rb.AddForce(Vector3.up * jumpForce * 0.3f);
            if (isWallLeft && Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * jumpForce * 3.2f); rb.AddForce(Vector3.up * jumpForce * 0.3f);

            //Adding forward force
            rb.AddForce(orientation.forward * jumpForce * 1f);

            //rb.velocity = Vector3.zero;
        }
    }
    private void Dash()
    {
        if (readyToDash)
        {

            //Vector2 mag = FindVelRelativeToLook();
            //float xMag = mag.x, yMag = mag.y;

            //float multiplier = 1f, multiplierV = 1f;

            //rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
            //rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);

            readyToDash = false;

            if (grounded && !crouching)
            {
                rb.AddForce(orientation.transform.forward * dashForce * 5);
            }
            if (!grounded && !crouching)
            {
                rb.AddForce(orientation.transform.forward * dashForce * 0.3f);
            }
            dashCDUI = 0.0f;
            //rb.AddForce(orientation.transform.right * dashForce);
            //rb.AddForce(orientation.transform.right * dashForce * 0.5f);

            //Vector3 vel = rb.velocity;
            //if (rb.velocity.x < 0.5f && rb.velocity.z < 0.5f)
            //    rb.velocity = new Vector3(0, vel.y, 0);
            //else if (rb.velocity.x > 0 && rb.velocity.z > 0)
            //    rb.velocity = new Vector3(vel.x / 2, vel.y, vel.z / 2);

            Invoke(nameof(ResetDash), dashCooldown);
        }
    }

    private void WallRunInput()
    {
        if (Input.GetKey(KeyCode.D) && isWallRight) StartWallRun();
        if (Input.GetKey(KeyCode.A) && isWallLeft) StartWallRun();
    }

    private void StartWallRun()
    {
        rb.useGravity = false;
        isWallRunning = true;

        if (rb.velocity.magnitude <= maxWallrunSpeed)
        {
            rb.AddForce(orientation.forward * wallrunForce * Time.deltaTime);

            if (isWallRight)
                rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
            else
                rb.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);
        }
    }

    private void StopWallRun()
    {
        rb.useGravity = true;
        isWallRunning = false;
    }

    private void CheckForWall()
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, whatIsWall);

        if (!isWallRight && !isWallLeft) StopWallRun();

    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ResetDash()
    {
        readyToDash = true;
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //find where player is looking at currently
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //ensuring no over/under rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Performing rotation of cam 
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, wallRunCameraTilt);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);

        //while wallrunning
        //tilting cam in half a sec
        if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && isWallRunning && isWallRight)
            wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 5;
        if (Math.Abs(wallRunCameraTilt) < maxWallRunCameraTilt && isWallRunning && isWallLeft)
            wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 5;

        //tilting cam back to regular pos
        if (wallRunCameraTilt > 0 && !isWallRight && !isWallLeft)
            wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;
        if (wallRunCameraTilt < 0 && !isWallRight && !isWallLeft)
            wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;

    }

    private void CounterMovement(float x, float y, Vector2 mag)
    {
        if (!grounded || jumping) return;

        //slowing down aft player slides
        if (crouching)
        { 
            //if (slideForce < 200)
            //{
                //float multiplier = 1f, multiplierV = 1f;

                //rb.AddForce(orientation.transform.forward * y * crouchMoveSpeed * Time.deltaTime * multiplier * multiplierV);
                //rb.AddForce(orientation.transform.right * x * crouchMoveSpeed * Time.deltaTime * multiplier);
            //}
            //else
            //{
                //rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            //}
            float multiplier = 1f, multiplierV = 1f;

            rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime * multiplier * multiplierV);
            rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime * multiplier);

            return;
        }

        //Counter movement
        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        //limit diagonal running
        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * maxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;

    private void OnCollisionStay(Collision other)
    {
        //checking for ground
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //collision
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        //Invoke ground/wall cancel
        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }

}
