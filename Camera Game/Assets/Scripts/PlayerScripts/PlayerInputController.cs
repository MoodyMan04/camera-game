using UnityEngine;

// Moody 20230713
// Code taken / inspired by work done in the NDSU chapter of the ACM
/*
 * Class used to register input from mouse and keyboard for interacting with the player controllers 
 */

namespace PlayerScripts
{
    public class PlayerInputController : MonoBehaviour
    {
        // Variables
        [SerializeField] PlayerController playerController;

        private bool _sprinting;
        private bool _crouching;
        
        // Start is called before the first frame update
        void Start()
        {
            _sprinting = playerController.Sprinting;
            _crouching = playerController.Crouching;
        }

        // Method called by Player Controller in Update Method
        internal void GetInput()
        {
            // Get mouse input
            // Time.deltaTime gets time between updates, this here helps prevents faster sensitivity due to higher frame rates
            playerController.MouseX = Input.GetAxis("Mouse X") * playerController.mouseSens * Time.deltaTime; 
            playerController.MouseY = Input.GetAxis("Mouse Y") * playerController.mouseSens * Time.deltaTime;
            
            // Get Keyboard input
            playerController.HorizontalMove = Input.GetAxis("Horizontal");
            playerController.VerticalMove = Input.GetAxis("Vertical");
            if (playerController.HorizontalMove != 0.0f || playerController.VerticalMove != 0.0f)
                playerController.Moving = true;
            else
                playerController.Moving = false;

            if (!_crouching)
            {
                playerController.Sprinting = Input.GetButton("Sprint");
                _sprinting = playerController.Sprinting;
            }
            if (!_sprinting)
            {
                playerController.Crouching = Input.GetButton("Crouch");
                _crouching = playerController.Crouching;
            }

            playerController.Interacting = Input.GetButton("Interact");

        }
    }
}
