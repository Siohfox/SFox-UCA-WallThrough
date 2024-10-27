using UnityEditor;
using UnityEngine;

namespace WallThrough.Utility
{
    [CustomEditor(typeof(PassiveObjectMovement))]
    public class PassiveObjectMovementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            PassiveObjectMovement movement = (PassiveObjectMovement)target;

            // Show movement types array
            EditorGUILayout.PropertyField(serializedObject.FindProperty("movementTypes"), true);

            serializedObject.ApplyModifiedProperties();

            // Show specific fields based on enabled movement types
            foreach (var type in movement.movementTypes)
            {
                switch (type)
                {
                    case MovementType.Floating:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("floatAmplitude"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("floatFrequency"));
                        break;

                    case MovementType.Rotating:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationSpeed"));
                        break;

                    case MovementType.Vertical:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("lerpHeight"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("lerpSpeed"));
                        break;

                    case MovementType.Swaying:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("swayAmplitude"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("swayFrequency"));
                        break;

                    case MovementType.Bobbing:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("bobbingAmplitude"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("bobbingFrequency"));
                        break;

                    case MovementType.Zigzag:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("zigzagDistance"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("zigzagSpeed"));
                        break;

                    case MovementType.Circular:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("circleRadius"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("circleSpeed"));
                        break;

                    case MovementType.Scaling:
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("scaleAmplitude"));
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("scaleSpeed"));
                        break;
                }
            }

            // Apply changes to the serializedProperty
            serializedObject.ApplyModifiedProperties();
        }
    }
}