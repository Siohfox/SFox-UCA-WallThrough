using UnityEngine;
using TMPro;
using WallThrough.Gameplay;

namespace WallThrough.UI
{
    /// <summary>
    /// Manages the user interface for displaying objectives.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text objectiveText; // Text component for displaying objectives
        [SerializeField] private TMP_Text wallObjectiveText; // Text component specifically for wall objectives

        private void OnEnable()
        {
            ObjectiveManager.OnObjectiveCompleted += UpdateObjectiveUI; // Subscribe to objective completion events
        }

        private void OnDisable()
        {
            ObjectiveManager.OnObjectiveCompleted -= UpdateObjectiveUI; // Unsubscribe from objective completion events
        }

        /// <summary>
        /// Updates the UI to reflect the completed objective.
        /// </summary>
        /// <param name="objective">The name of the completed objective.</param>
        private void UpdateObjectiveUI(string objective)
        {
            objectiveText.text = $"Objective Complete: {objective}/5"; // Update to reflect the actual maximum objective count
        }
    }
}
