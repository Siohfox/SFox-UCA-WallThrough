using UnityEngine;

namespace WallThrough.Utility
{
    public enum MovementType
    {
        Floating,
        Rotating,
        Vertical,
        Swaying,
        Bobbing,
        Zigzag,
        Circular,
        Scaling
    }

    public class PassiveObjectMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        public MovementType[] movementTypes;

        // Floating settings
        [Header("Floating Settings")]
        public float floatAmplitude = 0.5f;
        public float floatFrequency = 1f;
        public bool randomizeFloating = false;
        public float minFloatAmplitude = 0.1f;
        public float maxFloatAmplitude = 1f;
        public float minFloatFrequency = 0.5f;
        public float maxFloatFrequency = 2f;

        // Vertical movement settings
        [Header("Vertical Movement Settings")]
        public float lerpHeight = 1f;
        public float lerpSpeed = 1f;
        public bool randomizeVertical = false;
        public float minLerpHeight = 0.5f;
        public float maxLerpHeight = 2f;
        public float minLerpSpeed = 0.5f;
        public float maxLerpSpeed = 2f;

        // Swaying settings
        [Header("Swaying Settings")]
        public float swayAmplitude = 0.5f;
        public float swayFrequency = 1f;
        public bool randomizeSwaying = false;
        public float minSwayAmplitude = 0.1f;
        public float maxSwayAmplitude = 1f;
        public float minSwayFrequency = 0.5f;
        public float maxSwayFrequency = 2f;

        // Bobbing settings
        [Header("Bobbing Settings")]
        public float bobbingAmplitude = 0.5f;
        public float bobbingFrequency = 1f;
        public bool randomizeBobbing = false;
        public float minBobbingAmplitude = 0.1f;
        public float maxBobbingAmplitude = 1f;
        public float minBobbingFrequency = 0.5f;
        public float maxBobbingFrequency = 2f;

        // Zigzag settings
        [Header("Zigzag Settings")]
        public float zigzagDistance = 1f;
        public float zigzagSpeed = 1f;
        public bool randomizeZigzag = false;
        public float minZigzagDistance = 0.5f;
        public float maxZigzagDistance = 2f;
        public float minZigzagSpeed = 0.5f;
        public float maxZigzagSpeed = 2f;

        // Circular settings
        [Header("Circular Settings")]
        public float circleRadius = 1f;
        public float circleSpeed = 1f;
        public bool randomizeCircular = false;
        public float minCircleRadius = 0.5f;
        public float maxCircleRadius = 2f;
        public float minCircleSpeed = 0.5f;
        public float maxCircleSpeed = 2f;

        // Scaling settings
        [Header("Scaling Settings")]
        public float scaleAmplitude = 0.1f;
        public float scaleSpeed = 1f;
        public bool randomizeScaling = false;
        public float minScaleAmplitude = 0.01f;
        public float maxScaleAmplitude = 0.5f;
        public float minScaleSpeed = 0.5f;
        public float maxScaleSpeed = 2f;

        // Rotation settings
        [Header("Rotation Settings")]
        public float rotationSpeed = 30f; // Degrees per second

        private Vector3 startScale;
        private Vector3 startPosition;
        private Vector3 targetPosition;
        private bool movingUp = true;

        private void Start()
        {
            startPosition = transform.position;
            targetPosition = startPosition + new Vector3(0, lerpHeight, 0);
            startScale = transform.localScale;

            // Randomize values if enabled
            if (randomizeFloating)
            {
                floatAmplitude = Random.Range(minFloatAmplitude, maxFloatAmplitude);
                floatFrequency = Random.Range(minFloatFrequency, maxFloatFrequency);
            }

            if (randomizeVertical)
            {
                lerpHeight = Random.Range(minLerpHeight, maxLerpHeight);
                lerpSpeed = Random.Range(minLerpSpeed, maxLerpSpeed);
            }

            if (randomizeSwaying)
            {
                swayAmplitude = Random.Range(minSwayAmplitude, maxSwayAmplitude);
                swayFrequency = Random.Range(minSwayFrequency, maxSwayFrequency);
            }

            if (randomizeBobbing)
            {
                bobbingAmplitude = Random.Range(minBobbingAmplitude, maxBobbingAmplitude);
                bobbingFrequency = Random.Range(minBobbingFrequency, maxBobbingFrequency);
            }

            if (randomizeZigzag)
            {
                zigzagDistance = Random.Range(minZigzagDistance, maxZigzagDistance);
                zigzagSpeed = Random.Range(minZigzagSpeed, maxZigzagSpeed);
            }

            if (randomizeCircular)
            {
                circleRadius = Random.Range(minCircleRadius, maxCircleRadius);
                circleSpeed = Random.Range(minCircleSpeed, maxCircleSpeed);
            }

            if (randomizeScaling)
            {
                scaleAmplitude = Random.Range(minScaleAmplitude, maxScaleAmplitude);
                scaleSpeed = Random.Range(minScaleSpeed, maxScaleSpeed);
            }
        }

        private void Update()
        {
            foreach (var type in movementTypes)
            {
                switch (type)
                {
                    case MovementType.Floating:
                        Float();
                        break;
                    case MovementType.Rotating:
                        Rotate();
                        break;
                    case MovementType.Vertical:
                        VerticalMovement();
                        break;
                    case MovementType.Swaying:
                        Sway();
                        break;
                    case MovementType.Bobbing:
                        Bob();
                        break;
                    case MovementType.Zigzag:
                        Zigzag();
                        break;
                    case MovementType.Circular:
                        CircularMotion();
                        break;
                    case MovementType.Scaling:
                        Scale();
                        break;
                }
            }
        }

        private void Float()
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        private void Rotate()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }

        private void VerticalMovement()
        {
            float step = lerpSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, movingUp ? targetPosition : startPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                movingUp = false;
            }
            else if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                movingUp = true;
            }
        }

        private void Sway()
        {
            float newX = startPosition.x + Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        private void Bob()
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        private void Zigzag()
        {
            float newX = startPosition.x + Mathf.Sin(Time.time * zigzagSpeed) * zigzagDistance;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        private void CircularMotion()
        {
            float angle = Time.time * circleSpeed;
            float newX = startPosition.x + Mathf.Cos(angle) * circleRadius;
            float newZ = startPosition.z + Mathf.Sin(angle) * circleRadius;
            transform.position = new Vector3(newX, transform.position.y, newZ);
        }

        private void Scale()
        {
            float scaleFactor = 1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmplitude;
            transform.localScale = startScale * scaleFactor;
        }
    }
}
