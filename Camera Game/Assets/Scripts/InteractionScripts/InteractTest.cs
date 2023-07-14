using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

// Moody 20230714
/*
 * Class to test implementation of interaction
 */

namespace InteractionScripts
{
    public class InteractTest : MonoBehaviour, INteractable
    {
        // Variables
        [SerializeField] Material material1;
        [SerializeField] Material material2;

        private Renderer _rend;
        
        private bool _interacted = false;
        
        // Start is called before the first frame update
        void Start()
        {
            _rend = GetComponent<Renderer>();
            _rend.enabled = true;
            _rend.sharedMaterial = material1;
        }
        
        public void OnInteract()
        {
            if (!_interacted)
            {
                _rend.sharedMaterial = _rend.sharedMaterial == material1 ? material2 : material1;
                
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
