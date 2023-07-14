using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Moody 20230712
// Code inspired by Comp-3 Interactive
/*
 * Class to implement the stamina bar for the player
 */

namespace PlayerScripts
{
    public class PlayerStaminaController : MonoBehaviour
    {
        // Variables
        [SerializeField] PlayerController playerController;
        [SerializeField] Image staminaBarBackgroundImage;
        [SerializeField] Image staminaBarFillImage;
        
        public Slider staminaBar;

        public static PlayerStaminaController Instance;

        private WaitForSeconds _regenTick = new(0.05f);
        private Coroutine _regen;

        private void Awake()
        {
            // Make sure that there is only one stamina bar
            if (Instance != null)
            {
                Debug.Log("An instance of the stamina bar already exists");
            }

            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            playerController.Stamina = playerController.maxStamina;
            staminaBar.maxValue = playerController.maxStamina;
            staminaBar.value = playerController.maxStamina;
        }
        
        // Method to consume stamina
        internal void UseStamina()
        {
            
            // If Stamina can be spent, spend stamina and start regen if not spending any more
            if (playerController.Stamina - playerController.staminaDrain >= 0)
            {
                playerController.Stamina -= playerController.staminaDrain;
                staminaBar.value = playerController.Stamina;

                if (_regen != null)
                {
                    StopCoroutine(_regen);
                }

                _regen = StartCoroutine(RegenStamina());
            }
            // If Stamina ran out, prevent more stamina use
            else
            {
                playerController.StaminaEmpty = true;
            }
        }
        
        private IEnumerator RegenStamina()
        {
            yield return new WaitForSeconds(2);

            // While Stamina is less than max, regen Stamina
            while (playerController.Stamina < playerController.maxStamina)
            {
                // If crouching, regen quicker. Default regen otherwise
                if (playerController.Crouching)
                    playerController.Stamina += playerController.staminaRecoveryCrouching;
                else
                    playerController.Stamina += playerController.staminaRecovery;
                
                staminaBar.value = playerController.Stamina;

                // When Stamina is 50, allow Stamina Use 
                if (playerController.Stamina >= 50)
                    playerController.StaminaEmpty = false;
                
                yield return _regenTick;
            }

            // Make sure stamina does not go over limit
            if (playerController.Stamina > playerController.maxStamina)
                playerController.Stamina = playerController.maxStamina;
            
            _regen = null;
        }
        
        // Method called in PlayerController in Update Method
        internal void DisplayStamina()
        {
            // Disable image for stamina bar when full
            staminaBarBackgroundImage.enabled = staminaBarFillImage.enabled = !(Math.Abs(playerController.Stamina - playerController.maxStamina) < 1f);

            // Get temp colors for stamina bar
            var tempBackgroundColor = staminaBarBackgroundImage.color;
            var tempFillColor = staminaBarFillImage.color;
            
            // If stamina is empty, make image more transparent until not empty (Stamina 50% full)
            if (playerController.StaminaEmpty)
                tempBackgroundColor.a = tempFillColor.a = 0.5f;
            else
                tempBackgroundColor.a = tempFillColor.a = 1f;
            
            // Change color accordingly
            staminaBarBackgroundImage.color = tempBackgroundColor;
            staminaBarFillImage.color = tempFillColor;
        }
    }
}
