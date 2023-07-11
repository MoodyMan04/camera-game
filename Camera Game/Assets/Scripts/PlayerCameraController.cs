using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moody 20230710
// Code taken / inspired by Brackeys
/**
 * Class to implement the ability to move the main camera as a first person player
 */
public class PlayerCameraController : MonoBehaviour
{
    
    public float mouseSen = 100f;

    public Transform playerBody;

    private float _xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime gets time between updates, this here helps prevents faster sensitivity due to higher frame rates
        float mouseX = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;
        
        // Restrict mouseY rotation to not go upside down
        _xRotation -= mouseY;
        _xRotation = Math.Clamp(_xRotation, -90f, 90f);
        
        // Move camera accordingly
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        
    }
}
