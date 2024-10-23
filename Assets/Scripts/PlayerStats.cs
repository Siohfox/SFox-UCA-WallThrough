using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.UI;

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

        // Start is called before the first frame update
        void Start()
        {
            health = maxHealth;
            breath = breathMax;
            invincibilityTimer = invincibilityTimerMax;
        }

        // Update is called once per frame
        void Update()
        {
            if(invincibilityTimer >= 0f)
            {
                invincibilityTimer -= 1f * Time.deltaTime;
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
            if(invincibilityTimer <= 0 && breath <= 0)
            {
                health -= damageAmount;
                invincibilityTimer = invincibilityTimerMax;
            }   
        }

        private void HandlePlayerDeath()
        {
            transform.parent.gameObject.SetActive(false);
        }


    }
}
