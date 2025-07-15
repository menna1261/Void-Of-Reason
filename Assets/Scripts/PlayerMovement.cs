using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public GlobalRefrences globalRefrences;

    public float iiiiiiiii;
    public float speed = 20f;
    public float runSpeed = 6f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.5f;
    public float fallMultiplier = 2.5f;

    bool isGrounded;
    bool isMoving;
    //bool isWalking;
    //bool isRunning;

    Vector3 velocity = new Vector3(0, -2f, 0);
    Vector3 lastPosition = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log($"isGrounded : {isGrounded}");
        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = 0f;
        }

        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Check movement states
        isMoving = move.magnitude > 0.1f && isGrounded;
        globalRefrences.isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
        globalRefrences.isWalking = isMoving && !globalRefrences.isRunning;
        
  
        globalRefrences.isMoving = isMoving;

        // Apply movement
        float currentSpeed = globalRefrences.isRunning ? runSpeed : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        if (velocity.y < 0)
        {
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        // Play walking sound if moving
        if (globalRefrences.isWalking)
        {
            if (!SoundManager.instance.Walkingsound.isPlaying)
                SoundManager.instance.Walkingsound.Play();
        }
        else
        {
            SoundManager.instance.Walkingsound.Stop();
        }

        // Optional: Debug log
        // Debug.Log($"isMoving: {isMoving}, isWalking: {globalRefrences.isWalking}, isRunning: {globalRefrences.isRunning}");
    }
}

