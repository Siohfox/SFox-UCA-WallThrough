using System;
using UnityEngine;

namespace WallThrough.Events
{
    public static class GameEvents
    {
        public static event Action<string> OnObjectiveCompleted;
        public static event Action<string> OnObjectiveUpdate;

        public static void TriggerObjectiveCompleted(string objective)
        {
            OnObjectiveCompleted?.Invoke(objective);
        }

        public static void TriggerObjectiveUpdate(string message)
        {
            OnObjectiveUpdate?.Invoke(message);
        }
    }
}
