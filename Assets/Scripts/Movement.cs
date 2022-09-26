using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;


public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 15f;
    private Vector3 move;

    public float gravity = -9.81f;
    public float JumpForce = 2f;
    private Vector3 velocity;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    InputAction movement;
    InputAction jump;

    public FixedJoystick joystick;

    void Start()
    {
        jump = new InputAction("Jump", binding: "<keyboard>/space");
        movement = new InputAction("PlayerMovement");
        movement.AddCompositeBinding("Dpad")
        .With("Up", "<keyboard>/w")
        .With("Up", "<keyboard>/upArrow")
        .With("Down", "<keyboard>/s")
        .With("Down", "<keyboard>/downArrow")
        .With("Left", "<keyboard>/a")
        .With("Left", "<keyboard>/leftArrow")
        .With("Right", "<keyboard>/d")
        .With("Right", "<keyboard>/rightArrow");

        movement.Enable();
        jump.Enable();

    }

    
    void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); 

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        if (isGrounded)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
               
                Jump();
            }

        }

        else
        {

            velocity.y += gravity * Time.deltaTime;
        }


        controller.Move(velocity * Time.deltaTime);


    }

    private void Jump()
    {
       velocity.y = JumpForce;
    }

}
