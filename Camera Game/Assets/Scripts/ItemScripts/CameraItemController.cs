using PlayerScripts;
using UnityEngine;

// Moody 20230717
/*
 * Class to implement the VHS camera item
 */

namespace ItemScripts
{
    public class CameraItemController : MonoBehaviour
    {
        // Variables
        public static CameraItemController Instance;
        
        [Header("External Classes")] 
        [SerializeField] PlayerController playerController;
        
        [Header("Variables (Local)")]
        [SerializeField] float maxBatteryLife = 100f;
        [SerializeField] float maxNumBatteries = 10f;
        [SerializeField] float maxZoom = 10f;

        internal float BatteryLife;
        internal float NumBatteries;
        internal float Zoom;
        internal bool CanUseCamera = false;
        internal bool CameraInUse;

        // Start is called before the first frame update
        void Start()
        {
            // Make sure there is only one instance of the camera item controller
            if (Instance != null)
            {
                Debug.Log("A CameraItemController instance already exists");
            }

            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
