using System;
using UnityEngine;

// Moody 20230712
// Code taken / inspired by Brackeys, a multitude of other youtubers, and work at the NDSU chapter of the ACM
/*
* Class to implement the ability to move the player as a first person player
*/

namespace PlayerScripts
{
    public class PlayerMovementController : MonoBehaviour
    {
        // Variables
        [SerializeField] PlayerController playerController;
        

        private Vector3 _initialCameraPos;
        
        private float _currentHeight;
        private Vector3 _velocity;
        private Vector3 _move;
        
        private bool IsCrouching => playerController.standingHeight - _currentHeight > .1f;

        // Start is called before the first frame update
        private void Start()
        {
            _initialCameraPos = playerController.cameraHolder.transform.localPosition;
        }

        // Method called by PlayerController in Update Method
        internal void MovePlayer()
        {
            Grounded();
            LocationMove();
            ActionType();
            SmoothCrouch();
            Move();
        }
        
        // Method for checking if player is on ground
        private void Grounded()
        {
            playerController.Grounded = Physics.CheckSphere(playerController.groundCheck.transform.position, playerController.groundDistance, playerController.mapMask);
            if (playerController.Grounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
        }
        
        // Method for getting location to move to
        private void LocationMove()
        {
            _move = (transform.right * playerController.HorizontalMove + transform.forward * playerController.VerticalMove);
            if (_move.magnitude > 1.0f)
            {
                _move = _move.normalized;
            }
        }
        
        // Method for deciding on action of player and change speed if needed
        private void ActionType()
        {
            // Check if sprinting, crouching, or neither
            if (playerController.Sprinting && !playerController.Crouching)
            {
                // Logic for sprint stamina
                // INSERT STAMINA LOGIC HERE
                playerController.Speed = playerController.sprintSpeed;
            }
            else if (!playerController.Sprinting && playerController.Crouching)
                playerController.Speed = playerController.crouchSpeed;
            else if (!playerController.Crouching && !playerController.Sprinting)
                playerController.Speed = playerController.defaultSpeed;
        }
        
        // Method for smooth crouch logic
        private void SmoothCrouch()
        {
            // Get height target to reach
            var heightTarget = playerController.Crouching ? playerController.crouchHeight : playerController.standingHeight;

            // If trying to un-crouch, first see if possible. If not, continue crouching 
            if (IsCrouching && !playerController.Crouching)
            {
                var castOrigin = transform.position + new Vector3(0, _currentHeight / 2, 0);
                if (Physics.Raycast(castOrigin, Vector3.up, out RaycastHit hit, 0.2f))
                {
                    var distanceToCeiling = hit.point.y - castOrigin.y;
                    heightTarget = Mathf.Max(_currentHeight + distanceToCeiling - 0.1f, playerController.crouchHeight);
                }
            }

            // If height target is already the current height, skip
            if (!Mathf.Approximately(heightTarget, _currentHeight))
            {
                // Smooth transition to crouching / un-crouching
                var crouchDelta = playerController.crouchTransitionSpeed * Time.deltaTime;
                _currentHeight = Mathf.Lerp(_currentHeight, heightTarget, crouchDelta);

                var halfHeightDiff = new Vector3(0, (playerController.standingHeight - _currentHeight) / 2, 0);
                var newCameraPos = _initialCameraPos - halfHeightDiff;

                playerController.Height = _currentHeight;
            }
        }
        
        // Method for moving player
        private void Move()
        {
            playerController.charController.Move(_move * (playerController.Speed * Time.deltaTime));

            // Move player with gravity if falling accordingly
            _velocity.y += playerController.gravity * Time.deltaTime;

            playerController.charController.Move(_velocity * Time.deltaTime);
        }
    }
}
