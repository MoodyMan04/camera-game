using UnityEngine;

// Moody 20230712
// Code inspired by work at the NDSU chapter of the ACM
/*
* This class acts as a class that controls all other player scripts
*/

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        [Header("Internal Classes")]
        // [SerializeField] internal PlayerInputController inputController;
        [SerializeField] internal PlayerMovementController movementController;
        [SerializeField] internal PlayerCameraController cameraController;
        [SerializeField] internal CharacterController charController;
        [SerializeField] internal GameObject groundCheck;

        [Header("External Classes")] 
        [SerializeField] internal Camera mainCamera;

        [Header("Variables (Local)")]
        // [SerializeField]'s

        internal bool Sprinting;
        internal bool Moving;
        internal bool Grounded;
    
        // Start is called before the first frame update
        void Start()
        {
            // Make sure there is only one instance of the player
            if (Instance != null)
            {
                Debug.Log("A PlayerScript instance already exists");
            }

            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            // inputController.getInput();
            movementController.MovePlayer();
            cameraController.MoveCamera();
        }
    }
}
