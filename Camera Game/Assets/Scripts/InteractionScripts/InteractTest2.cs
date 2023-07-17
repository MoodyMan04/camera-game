using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

// Moody 20230717
/*
 * Class to test implementation of interaction, mesh render, and box collider physics
 */

namespace InteractionScripts
{
    public class InteractTest2 : MonoBehaviour, INteractable
    {
        // Variables
        private MeshRenderer _meshRenderer;
        private MeshCollider _meshCollider;
        
        private bool _interacted = false;
        private bool _invisible = false;
        
        // Start is called before the first frame update
        void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.enabled = true;
            _meshCollider = GetComponent<MeshCollider>();
            _meshCollider.enabled = true;
        }
        
        public void OnInteract()
        {
            if (!_interacted)
            {
                if (!_invisible)
                {
                    _meshRenderer.enabled = false;
                    _meshCollider.enabled = false;
                }
                else
                {
                    _meshRenderer.enabled = true;
                    _meshCollider.enabled = true;
                }
                _interacted = true;
                Invoke(nameof(ResetInteracted), 3);
            }
        }

        private void ResetInteracted()
        {
            _interacted = false;
        }
    }
}