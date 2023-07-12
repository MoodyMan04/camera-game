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
        public PlayerController playerController;
    
        public float mouseSen = 1000f;
        public float rotAmount = 2f;

        public Animator camAnim;
    
        public Transform playerBody;

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
        
            // Time.deltaTime gets time between updates, this here helps prevents faster sensitivity due to higher frame rates
            float mouseX = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime; 
            float mouseY = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;
        
            // Restrict mouseY rotation to not go upside down
            _xRotation -= mouseY;
            _xRotation = Math.Clamp(_xRotation, -90f, 90f);
        
            // Move camera accordingly
            Vector3 v = transform.rotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(_xRotation, 0, v.z);
            playerBody.Rotate(Vector3.up * mouseX);
        
        }

        // Method to tilt camera
        void CameraRot()
        {
            // Tilt camera left or right upon movement
            float rotZ = -Input.GetAxis("Horizontal") * rotAmount;
        
            Quaternion finalRot = Quaternion.Euler(0, 0, rotZ);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, finalRot, 1f);
        
        }

        // Method to bob camera
        void CameraBob()
        {
            // Bob camera up and down when moving depending on type of movement (using animation)
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                    (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)))
                {
                    camAnim.ResetTrigger(Idle);
                    camAnim.SetTrigger(Sprint);
                }
                else
                {
                    camAnim.ResetTrigger(Idle);
                    camAnim.ResetTrigger(Walk);
                    camAnim.SetTrigger(Walk);

                }
            }
            else
            {
                camAnim.ResetTrigger(Walk);
                camAnim.ResetTrigger(Sprint);
                camAnim.SetTrigger(Idle);
            }

        }
    }
}
