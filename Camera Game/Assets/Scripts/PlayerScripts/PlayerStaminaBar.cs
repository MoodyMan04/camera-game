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
    public class PlayerStaminaBar : MonoBehaviour
    {
        // Variables
        [SerializeField] PlayerController playerController;

        public Slider staminaBar;

        public static PlayerStaminaBar Instance;

        private WaitForSeconds _regenTick = new WaitForSeconds(0.05f);
        private Coroutine _regen;

        private void Awake()
        {
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
            if (playerController.Stamina - playerController.staminaDrain >= 0)
            {
                playerController.Stamina -= playerController.staminaDrain;
                staminaBar.value = playerController.Stamina;

                if (_regen != null)
                    StopCoroutine(_regen);
                
                _regen = StartCoroutine(RegenStamina());
            }
        }

        private IEnumerator RegenStamina()
        {
            yield return new WaitForSeconds(2);

            while (playerController.Stamina < playerController.maxStamina)
            {
                playerController.Stamina += playerController.staminaRecovery;
                staminaBar.value = playerController.Stamina;
                yield return _regenTick;
            }

            _regen = null;
        }
    }
}
