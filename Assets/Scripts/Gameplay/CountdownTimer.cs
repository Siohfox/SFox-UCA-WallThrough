using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private float countDownTimer;
        [SerializeField] private float countDownTimerMax = 60.0f;
        
        void Start()
        {
            countDownTimer = countDownTimerMax;
        }

        private void Update()
        {
            countDownTimer -= Time.deltaTime;
        }
    }
}
