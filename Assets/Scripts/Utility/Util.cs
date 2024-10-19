using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
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
    }
}

