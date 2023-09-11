using ItemScripts;
using UnityEngine;

// Moody 20230717
/*
 * Class to enable the camera item for the player
 */

namespace InteractionScripts
{
    public class InteractCameraItem : MonoBehaviour
    {
        // Variables
        [SerializeField] CameraItemController cameraItem;

        public void OnInteract()
        {
            cameraItem.NumBatteries = 2f;
            cameraItem.CanUseCamera = true;
            Destroy(gameObject);
        }
    }
}
