using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // Moody 20230710
    // Code taken / inspired by Brackeys and work at the NDSU chapter of the ACM
    /**
    * Class to implement the ability to move the player as a first person player
    */

    public CharacterController controller;

    public float speed = 10f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 _velocity;
    private bool _isGrounded;
    
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

        // Move player
        controller.Move(move * (speed * Time.deltaTime));

        // Move player with gravity if falling accordingly
        _velocity.y += gravity * Time.deltaTime;

        controller.Move(_velocity * Time.deltaTime);
    }
}
