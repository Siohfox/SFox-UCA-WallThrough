using System;
using UnityEngine;
using WallThrough.Audio;
using WallThrough.Graphics; // Ensure this is included for CameraShake
using WallThrough.Core;

namespace WallThrough.Gameplay.Pawn
{
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 10, health;
        [SerializeField] private float invincibilityTimer, invincibilityTimerMax = 2f;
        [SerializeField] private bool aliveState = true;
        [SerializeField] private AudioClip damageTakenClip, deathClip, deathTune;
        private AudioSource src;

        private int previousHealth, previousBreath;

        public static event Action<int> OnHealthChange;
        public static event Action OnPlayerDeath;

        private void OnEnable()
        {
            CountdownTimer.OnTimerReachedMinimum += HandlePlayerDeath;
        }

        private void OnDisable()
        {
            CountdownTimer.OnTimerReachedMinimum -= HandlePlayerDeath;
        }

        private void Start()
        {
            health = maxHealth;
            aliveState = true;
            invincibilityTimer = invincibilityTimerMax;
            UpdateStats();
            src = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (previousHealth != health)
                UpdateStats();

            invincibilityTimer -= Time.deltaTime;

            if (health <= 0 && aliveState) HandlePlayerDeath();
        }

        public Vector3 Position => transform.position;
        public bool AliveState => aliveState;

        public void HealDamage(int amount) => health += amount;

        public void TakeDamage(int amount)
        {
            if (invincibilityTimer > 0) return;

            if (health > 0)
            {
                health -= amount;
                CameraShake.Instance.ShakeCamera(5.0f, 0.5f, "handheldNormalExtreme");
                AudioManager.Instance.PlaySound(damageTakenClip, 1, src);
                invincibilityTimer = invincibilityTimerMax;
            }
        }

        private void HandlePlayerDeath()
        {
            aliveState = false;
            AudioManager.Instance.PlaySound(deathClip, 1, src);
            AudioManager.Instance.PlaySound(deathTune, 1, src);
            AudioManager.Instance.StopPlayMusic();
            OnPlayerDeath?.Invoke();
            GameManager.Instance.currentGameState = GameManager.GameState.GameOver;
        }

        private void UpdateStats()
        {
            OnHealthChange?.Invoke(health);
            previousHealth = health;
        }
    }
}
