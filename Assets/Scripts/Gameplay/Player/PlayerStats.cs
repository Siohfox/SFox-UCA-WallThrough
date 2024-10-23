using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Graphics;

namespace WallThrough.Gameplay.Pawn
{
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int health;
        [SerializeField] private int breath;
        [SerializeField] private int breathMax = 10;
        [SerializeField] private float invincibilityTimer;
        [SerializeField] private float invincibilityTimerMax = 2f;
        [SerializeField] private bool drowningState;

        private int previousHealth = 0;
        private int previousBreath = 0;

        public static event Action<int> OnHealthChange;
        public static event Action<int> OnBreathChange;

        private static void UpdateHealth(int health)
        {
            OnHealthChange?.Invoke(health);
        }

        private static void UpdateBreath(int breath)
        {
            OnBreathChange?.Invoke(breath);
        }

        // Start is called before the first frame update
        void Start()
        {
            drowningState = false;
            health = maxHealth;
            breath = breathMax;
            invincibilityTimer = invincibilityTimerMax;
            UpdateHealth(health);
            UpdateBreath(breath);
        }

        // Update is called once per frame
        void Update()
        {
            // Check if health has changed from the inspector
            if (previousHealth != health || previousBreath != breath)
            {
                UpdateHealth(health);
                UpdateBreath(breath);
                previousHealth = health;
                previousBreath = breath;
            }

            if (invincibilityTimer >= 0f)
            {
                invincibilityTimer -= 1f * Time.deltaTime;
            }

            if(!drowningState && breath < breathMax && invincibilityTimer <= 0)
            {
                breath++;
                invincibilityTimer = invincibilityTimerMax;
            }

            if (health <= 0)
            {
                HandlePlayerDeath();
            }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }

        public void HealDamage(int damageAmount)
        {
            health += damageAmount;
        }

        public void TakeDamage(int damageAmount)
        {
            // If drowning, remove breath instead of health
            if(invincibilityTimer <= 0 && breath > 0 && drowningState == true)
            {
                breath -= damageAmount;
                invincibilityTimer = invincibilityTimerMax;
            }

            // If no breath, take health instead
            if(invincibilityTimer <= 0 && breath <= 0)
            {
                health -= damageAmount;
                CameraShake.Instance.ShakeCamera(5.0f, 0.5f, "handheldNormalExtreme");
                invincibilityTimer = invincibilityTimerMax;
            }   
        }

        private void HandlePlayerDeath()
        {
            transform.parent.gameObject.SetActive(false);
        }

        public void SetDrowningState(bool drowning)
        {
            drowningState = drowning;
        }
    }
}
