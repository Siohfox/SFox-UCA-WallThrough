using WallThrough.Gameplay.Interactable;
using UnityEngine;
using WallThrough.Audio;


namespace WallThrough.Gameplay
{
    public class Collectable : Objective, IInteractable
    {
        [SerializeField]
        private AudioClip collectClip;

        // Floating and rotation settings
        [SerializeField] private float floatAmplitude = 0.5f; // Height of the float
        [SerializeField] private float floatFrequency = 1f; // Speed of the float
        [SerializeField] private float rotationSpeed = 30f; // Degrees per second
        private Vector3 initialPosition;

        private void Awake()
        {
            src = GetComponent<AudioSource>();
            if (!src) Debug.LogWarning("No audio source found");

            SetObjectiveType(ObjectiveType.Collectable);

            // Store the initial position to use as the base for floating
            initialPosition = transform.position;
        }

        private void Update()
        {
            // Floating effect
            float newY = initialPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // Continuous rotation
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }

        public void InteractionStart()
        {
            if (!IsCompleted) // Ensure the objective isn't already completed
            {
                // Play sound effect
                AudioManager.Instance.PlaySoundAtPosition(collectClip, 1.0f, transform.position);

                // Complete the objective
                base.CompleteObjective();

                // Disable the collectable
                gameObject.SetActive(false);
            }
        }

        public void InteractionEnd()
        {
            // Logic for ending the interaction
        }
    }
}

