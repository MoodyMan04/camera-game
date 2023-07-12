using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // Moody 20230711
    // Code taken / inspired by Brackeys and work at the NDSU chapter of the ACM
    /**
    * Class to implement the ability to move the player as a first person player
    */

    public CharacterController controller;

    public float defaultSpeed = 4f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 2f;
    public float crouchHeight = 1f;     
    public float gravity = -15f;
    public float speed = 4f;            // Do not adjust manually
    public float defaultHeight = 2f;  // Number should match character controller set height


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _sprinting = false;
    private bool _crouching = false;

    // Update is called once per frame
    void Update()
    {

        // Check to see if player is on a surface
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        // Get location to move to based on player direction and location
        Vector3 move = (transform.right * x + transform.forward * z);
        if (move.magnitude > 1.0f)
        {
            move = move.normalized;
        }

        // Check if shift button is pressed. If so, sprint
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && _crouching == false)
        {
            speed = sprintSpeed;
            _sprinting = true;
        }

        if ((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) && _crouching == false)
        {
            speed = defaultSpeed;
            _sprinting = false;
        }
        
        // Check if control button is pressed. If so, crouch
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && _sprinting == false)
        {
            speed = crouchSpeed;
            controller.height = crouchHeight;
            _crouching = true;
        }

        if ((Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)) && _sprinting == false)
        {
            speed = defaultSpeed;
            controller.height = defaultHeight;
            _crouching = false;
        }
        
        // Move player
        controller.Move(move * (speed * Time.deltaTime));

        // Move player with gravity if falling accordingly
        _velocity.y += gravity * Time.deltaTime;

        controller.Move(_velocity * Time.deltaTime);
    }
}
