using System;
using UnityEngine;
using WallThrough.UI;

namespace WallThrough.Gameplay
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float countdownTimerMax = 60f;
        [SerializeField] private float countdownTimerMin = 0f;
        [SerializeField] private bool startStopTimer = false;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private float countdownTimer;

        public static event Action OnTimerReachedMinimum;

        private void Start()
        {
            countdownTimer = countdownTimerMax;

            if (!uiManager)
            {
                uiManager = FindObjectOfType<UIManager>();
                if (!uiManager)
                {
                    Debug.LogError("CountdownTimer: UIManager not found.");
                }
            }
        }

        private void Update()
        {
            if (startStopTimer && countdownTimer > countdownTimerMin)
            {
                countdownTimer -= Time.deltaTime;
                uiManager?.UpdateTimer(countdownTimer);
            }
            else if (countdownTimer <= countdownTimerMin)
            {
                OnTimerReachedMinimum?.Invoke();
            }
        }
    }
}
