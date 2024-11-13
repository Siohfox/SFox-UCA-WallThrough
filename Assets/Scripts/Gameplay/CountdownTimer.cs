using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WallThrough.UI;

namespace WallThrough.Gameplay
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float countdownTimer;
        [SerializeField] private float countdownTimerMax = 60.0f;
        [SerializeField] private float countdownTimerMin = 0.0f;
        [SerializeField] private bool startStopTimer;

        [SerializeField] UIManager uiManager;

        public static event Action OnTimerReachedMinimum;
        
        void Start()
        {
            countdownTimer = countdownTimerMax;

            if (!uiManager)
            {
                try
                {
                    uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                }
                catch 
                {
                    Debug.Log("CountdownTimer cannot find UIManager");
                }
            }
        }

        private void Update()
        {
            if (countdownTimer > countdownTimerMin)
            {
                CountDownTimer(startStopTimer);
            }
            else
            {
                OnTimerReachedMinimum?.Invoke();
            }
        }

        private void CountDownTimer(bool startstop)
        {
            if (startstop)
            {
                countdownTimer -= Time.deltaTime;

                uiManager.UpdateTimer(countdownTimer);
            }
        }
    }
}
