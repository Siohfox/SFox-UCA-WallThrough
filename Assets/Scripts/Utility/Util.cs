using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Utility
{
    public static class Util
    {
        // Returns a random value based on enum length --- no longer enum
        public static T GetRandomEnumValue<T>() where T : Enum
        {
            Array enumValues = Enum.GetValues(typeof(T));
            int randomIndex = UnityEngine.Random.Range(0, enumValues.Length);
            return (T)enumValues.GetValue(randomIndex);
        }

        public static T MathSquare<T>(T numberToSquare) where T : IConvertible
        {
            // Convert to double for arithmetic operations
            double num = Convert.ToDouble(numberToSquare);
            return (T)Convert.ChangeType(num * num, typeof(T));  // Convert back to original type.
        }

        /// <summary>
        /// Attempts to find a given object, else debugs an error to console.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="caller"></param>
        /// <param name="reference"></param>
        /// <param name="message"></param>
        /// <returns>Reference to found object, or a debug error if no found object.</returns>
        public static T FindOrLogError<T>(MonoBehaviour caller, ref T reference, string message = null) where T : MonoBehaviour
        {
            if (reference == null)
            {
                reference = UnityEngine.Object.FindObjectOfType<T>();

                if (reference == null && caller != null)
                {
                    Debug.LogError($"[{caller.GetType().Name}] {message ?? $"Could not find {typeof(T).Name} in the scene."}");
                }
            }
            return reference;
        }

    }
}

