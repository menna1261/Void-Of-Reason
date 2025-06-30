using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 5f;
    public float gravity = -9.81f * 2; // Double Earth's gravity
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.5f;

    bool isMoving;
    bool isGrounded;

    Vector3 velocity = new Vector3(0, -2f, 0); // Start with slight downward velocity
    Vector3 lastPosition = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log("isGrounded: " + isGrounded);

        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0f)
        {
           // Debug.Log("Grounded and resetting velocity.");
            velocity.y = 0f; 
        }

        // Get movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump triggered");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply movement
        controller.Move(velocity * Time.deltaTime);
        

        // Check movement
        if (lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
            SoundManager.instance.Walkingsound.Play();

        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
    }
}
