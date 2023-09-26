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
                if (!playerController.Interacting) return;
                // Check if what we are interacting with can be interacted with
                if (hit.collider.GetComponent<INteractable>() != null)
                {
                    hit.collider.GetComponent<INteractable>().OnInteract();
                }
            }
            else
                interactImage.enabled = false;
        }
    }
}
