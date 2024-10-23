using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace WallThrough.Graphics
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get; private set; }

        private CinemachineVirtualCamera vcam;
        private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
        public NoiseSettings handheldNormalExtreme;
        public NoiseSettings SixDShake;

        private float shakeTimer;
        private float shakeTimerTotal;
        private float startingIntensity;

        private void Awake()
        {
            Instance = this;
            vcam = GetComponent<CinemachineVirtualCamera>();
            cinemachineBasicMultiChannelPerlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera(float intensity, float time, string noiseProfile)
        {
            if(noiseProfile == "SixDShake")
            {
                cinemachineBasicMultiChannelPerlin.m_NoiseProfile = SixDShake;
            }
            if(noiseProfile == "handheldNormalExtreme")
            {
                cinemachineBasicMultiChannelPerlin.m_NoiseProfile = handheldNormalExtreme;
            }   

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            startingIntensity = intensity;
            shakeTimerTotal = time;
            shakeTimer = time;
        }

        private void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - shakeTimer / shakeTimerTotal);
            }
        }
    }
}