using UnityEngine;
using UnityEngine.UI;
using System;
using WallThrough.Events;
using TMPro;


namespace WallThrough.UI
{
    public class UIManager : MonoBehaviour
    {
        public TMP_Text objectiveText; // Assign in the Inspector

        private void OnEnable()
        {
            GameEvents.OnObjectiveCompleted += UpdateObjectiveUI;
        }

        private void OnDisable()
        {
            GameEvents.OnObjectiveCompleted -= UpdateObjectiveUI;
        }

        private void UpdateObjectiveUI(string objective)
        {
            objectiveText.text = $"Objective Complete: {objective}/5"; // Should be updated to find objective max instead of hardcode
        }

        private void UpdateOtherUI(string message)
        {
            // Update other UI
        }
    }
}