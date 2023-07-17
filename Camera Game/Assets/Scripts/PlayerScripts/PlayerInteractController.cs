using InteractionScripts;
using UnityEngine;
using UnityEngine.UI;

// Moody 20230717
// Code taken / inspired by work done in the NDSU chapter of the ACM
/*
 * Class that will handle interaction with the interaction mask
 */

namespace PlayerScripts
{
    public class PlayerInteractController : MonoBehaviour
    {
        // Variables
        [SerializeField] PlayerController playerController;
        [SerializeField] Image interactImage;

        // Method called by PlayerController in Update Method
        internal void Interactable()
        {
            // Check if item is interactable and to interact if done
            if (Physics.Raycast(playerController.cameraHolder.transform.position, playerController.cameraHolder.transform.forward, out var hit,
                    playerController.interactRange, playerController.interactableMask))
            {
                interactImage.enabled = true;
                
                // Check if we are interacting
                if (playerController.Interacting)
                {
                    // Check what is being interacted with (PLACE ALL POSSIBLE INTERACTIONS HERE) ~ should be better way to do this (expensive)
                    if (hit.collider.GetComponent<InteractTest>() != null)
                        hit.collider.GetComponent<InteractTest>().OnInteract();
                    if(hit.collider.GetComponent<InteractTest2>() != null)
                        hit.collider.GetComponent<InteractTest2>().OnInteract();
                    if(hit.collider.GetComponent<InteractCameraItem>() != null)
                        hit.collider.GetComponent<InteractCameraItem>().OnInteract();
                    
                    
                }
            }
            else
                interactImage.enabled = false;
        }
    }
}
