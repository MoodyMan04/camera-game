using System;
using UnityEngine;

// Moody 20230712
// Code taken / inspired by Brackeys
/*
* Class to implement the ability to move the main camera as a first person player
*/

namespace PlayerScripts
{
    
    public class PlayerCameraController : MonoBehaviour
    {
        // Variables
        [SerializeField] PlayerController playerController;

        private float _xRotation = 0f;
        
        private static readonly int Walk = Animator.StringToHash("walk");
        private static readonly int Idle = Animator.StringToHash("idle");
        private static readonly int Sprint = Animator.StringToHash("sprint");

        // Start is called before the first frame update
        void Start()
        {
        
            // Lock cursor to center of screen
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Method called by PlayerController in Update Method
        internal void MoveCamera()
        {
            CameraRot();
            CameraBob();

            // Restrict mouseY rotation to not go upside down
            _xRotation -= playerController.MouseY;
            _xRotation = Math.Clamp(_xRotation, -90f, 90f);
        
            // Move camera accordingly
            Vector3 v = transform.rotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(_xRotation, 0, v.z);
            playerController.player.transform.Rotate(Vector3.up * playerController.MouseX);
        
        }

        // Method to tilt camera
        void CameraRot()
        {
            // Tilt camera left or right upon movement
            float rotZ = -Input.GetAxis("Horizontal") * playerController.camRotAmount;
        
            Quaternion finalRot = Quaternion.Euler(0, 0, rotZ);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, finalRot, 5f * Time.deltaTime);
        
        }

        // Method to bob camera
        void CameraBob()
        {
            // Bob camera up and down when moving depending on type of movement (using animation)
            if (playerController.Moving)
            {
                if (playerController.Sprinting && !playerController.Crouching)
                {
                    playerController.camAnim.ResetTrigger(Idle);
                    playerController.camAnim.SetTrigger(Sprint);
                }
                else
                {
                    playerController.camAnim.ResetTrigger(Idle);
                    playerController.camAnim.ResetTrigger(Walk);
                    playerController.camAnim.SetTrigger(Walk);

                }
            }
            else
            {
                playerController.camAnim.ResetTrigger(Walk);
                playerController.camAnim.ResetTrigger(Sprint);
                playerController.camAnim.SetTrigger(Idle);
            }

        }
    }
}
