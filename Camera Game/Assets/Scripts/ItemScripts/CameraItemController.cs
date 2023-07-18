using System;
using System.Runtime.InteropServices.WindowsRuntime;
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
        [SerializeField] float maxZoom = 4f;
        [SerializeField] float minZoom = 1f;
        [SerializeField] float zoomScale = 0.25f;
        [SerializeField] float reloadBatteryTime = 2f;

        internal float BatteryLife;
        internal float NumBatteries;
        internal float Zoom;
        internal bool CanUseCamera = false;
        internal bool CameraInUse = false;

        private Camera _camera;
        
        private float _timer = 0f;
        private float _defaultCameraFOV;
        private bool _cameraFirstTurnedOn;

        // Start is called before the first frame update
        void Start()
        {
            // Make sure there is only one instance of the camera item controller
            if (Instance != null)
            {
                Debug.Log("A CameraItemController instance already exists");
            }

            Instance = this;
            
            // Set default values
            BatteryLife = maxBatteryLife;
            Zoom = 1f;

            if (Camera.main != null) _camera = Camera.main;
            _defaultCameraFOV = _camera.fieldOfView;
        }

        // Update is called once per frame
        void Update()
        {
            GetCameraInput();
            ZoomCamera();
        }

        // Method to get input needed to control camera item (Called in CameraItemController Update Method)
        private void GetCameraInput()
        {
            if (CanUseCamera)
            {
                // Get open and close camera input (right click)
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    CameraInUse = !CameraInUse;
                }
                // When camera is in use, allow zooming
                if (CameraInUse)
                {
                    Zoom += Input.mouseScrollDelta.y * zoomScale;
                    
                    if (Zoom > maxZoom)
                        Zoom = maxZoom;
                    if (Zoom < minZoom)
                        Zoom = minZoom;
                }
                // Allow reloading batteries (built into GetInput Method)
                if (Input.GetKeyDown(KeyCode.R))
                    _timer = Time.time;
                else if (Input.GetKey(KeyCode.R))
                {
                    if (Time.time - _timer > reloadBatteryTime)
                    {
                        // Make sure event will not be set off again
                        _timer = float.PositiveInfinity;

                        // Reload battery life
                        if (NumBatteries != 0)
                        {
                            BatteryLife = maxBatteryLife;
                            --NumBatteries;
                        }
                    }
                }
                else
                    _timer = float.PositiveInfinity;


            }
        }

        // Method to zoom in camera (Called in CameraItemController Update Method)
        private void ZoomCamera()
        {
            if (CameraInUse)
            {
                // Get target FOV for camera
                float targetFOV = _defaultCameraFOV / Zoom;

                // If camera just opened, jump to current camera FOV without smooth transition
                if (_cameraFirstTurnedOn)
                {
                    _camera.fieldOfView = targetFOV;
                    _cameraFirstTurnedOn = false;
                    return;
                }
                
                // Otherwise, when in camera mode, smooth transition in zoom
                _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, targetFOV, 8f * Time.deltaTime);
            }
            else
            {
                _camera.fieldOfView = _defaultCameraFOV;
                _cameraFirstTurnedOn = true;
            }
        }
    }
}
