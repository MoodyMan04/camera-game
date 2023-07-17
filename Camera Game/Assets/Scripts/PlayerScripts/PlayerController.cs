using Unity.VisualScripting;
using UnityEngine;

// Moody 20230717
// Code taken / inspired by work at the NDSU chapter of the ACM
/*
* This class acts as a class that controls all other player scripts
*/

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        // Variables
        public static PlayerController Instance;

        [Header("Internal Classes")]
        [SerializeField] internal PlayerInputController inputController;
        [SerializeField] internal PlayerMovementController movementController;
        [SerializeField] internal PlayerCameraController cameraController;
        [SerializeField] internal PlayerInteractController interactController;
        [SerializeField] internal PlayerStaminaController staminaBar;
        [SerializeField] internal CharacterController charController;
        [SerializeField] internal GameObject groundCheck;

        [Header("External Classes")] 
        public GameObject player;
        public GameObject cameraHolder;

        [Header("Variables (Local)")] 
        [Header("Masks")]
        [SerializeField] internal LayerMask mapMask;
        [SerializeField] internal LayerMask interactableMask;
        [Header(("Control Settings"))]
        [SerializeField] internal float mouseSens = 1000f;
        [SerializeField] internal float groundDistance = 0.4f;
        [SerializeField] internal float camRotAmount = 2f;
        [Header("World Settings")]
        [SerializeField] internal float gravity = -15f;
        [Header("Player Settings")]
        [SerializeField] internal float standingHeight = 2f;
        [SerializeField] internal float crouchHeight = 1f;
        [SerializeField] internal float interactRange = 2f;
        [SerializeField] internal float defaultSpeed = 4f;
        [SerializeField] internal float sprintSpeed = 8f;
        [SerializeField] internal float crouchSpeed = 1.5f;
        [SerializeField] internal float defaultNoiseRadius = 4f;
        [SerializeField] internal float sprintNoiseRadius = 8f;
        [SerializeField] internal float crouchNoiseRadius = 2f;
        [SerializeField] internal float maxStamina = 100f;
        [SerializeField] internal float staminaDrain = 0.05f;
        [SerializeField] internal float staminaRecovery = 0.5f;
        [SerializeField] internal float staminaRecoveryCrouching = 0.75f;
        [SerializeField] internal float crouchTransitionSpeed = 10f;
        [SerializeField] internal float playerHealth = 100f;
        

        [Header("Animation")] 
        [SerializeField] internal Animator camAnim;

        internal float MouseX;
        internal float MouseY;
        internal float HorizontalMove;
        internal float VerticalMove;
        internal float Health;
        internal float Speed;
        internal float Stamina;
        internal float Height
        {
            get => charController.height;
            set => charController.height = value;
        }
        internal bool CanInput = true;
        internal bool Sprinting = false;
        internal bool Crouching = false;
        internal bool Interacting = false;
        internal bool StaminaEmpty = false;
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
            inputController.GetInput();
            movementController.MovePlayer();
            cameraController.MoveCamera();
            staminaBar.DisplayStamina();
            interactController.Interactable();
        }
    }
}
