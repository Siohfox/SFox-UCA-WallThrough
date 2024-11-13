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
        [SerializeField] private bool startStopTimer;
        
        void Start()
        {
            countdownTimer = countdownTimerMax;
        }

        private void Update()
        {
            CountDownTimer(startStopTimer);
        }

        private void CountDownTimer(bool startstop)
        {
            if (startstop)
            {
                countdownTimer -= Time.deltaTime;
            }
        }
    }
}
