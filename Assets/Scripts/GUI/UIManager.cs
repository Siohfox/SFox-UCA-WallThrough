using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using WallThrough.Gameplay;


namespace WallThrough.UI
{
    public class UIManager : MonoBehaviour
    {
        public TMP_Text objectiveText; // Assign in the Inspector

        private void OnEnable()
        {
            ObjectiveManager.OnObjectiveCompleted += UpdateObjectiveUI;
        }

        private void OnDisable()
        {
            ObjectiveManager.OnObjectiveCompleted -= UpdateObjectiveUI;
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