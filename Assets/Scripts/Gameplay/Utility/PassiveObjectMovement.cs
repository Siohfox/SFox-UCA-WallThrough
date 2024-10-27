using UnityEngine;

namespace WallThrough.Utility
{
    using UnityEngine;

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
        public float floatAmplitude = 0.5f;
        public float floatFrequency = 1f;

        // Vertical movement settings
        public float lerpHeight = 1f;
        public float lerpSpeed = 1f;

        // Swaying settings
        public float swayAmplitude = 0.5f;
        public float swayFrequency = 1f;

        // Bobbing settings
        public float bobbingAmplitude = 0.5f;
        public float bobbingFrequency = 1f;

        // Zigzag settings
        public float zigzagDistance = 1f;
        public float zigzagSpeed = 1f;

        // Circular settings
        public float circleRadius = 1f;
        public float circleSpeed = 1f;

        // Scaling settings
        public float scaleAmplitude = 0.1f;
        public float scaleSpeed = 1f;
        private Vector3 startScale;

        // Rotation settings
        public float rotationSpeed = 30f; // Degrees per second

        private Vector3 startPosition;
        private Vector3 targetPosition;
        private bool movingUp = true;

        private void Start()
        {
            startPosition = transform.position;
            targetPosition = startPosition + new Vector3(0, lerpHeight, 0);
            startScale = transform.localScale;
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