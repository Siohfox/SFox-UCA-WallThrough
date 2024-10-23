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
        [SerializeField] private ObjectiveManager objectiveManager;

        private void Start()
        {
            UpdateObjectiveUIOnStart();
        }

        private void OnEnable()
        {
            Objective.OnObjectiveCompleted += UpdateObjectiveUI; // Subscribe to objective completion events
        }

        private void OnDisable()
        {
            Objective.OnObjectiveCompleted -= UpdateObjectiveUI; // Unsubscribe from objective completion events
        }

        /// <summary>
        /// Updates the UI to reflect the completed objective.
        /// </summary>
        /// <param name="objective">The completed objective.</param>
        private void UpdateObjectiveUI(Objective objective)
        {
            objectiveText.text = $"Objective Complete: {objectiveManager.completedObjectives}/{objectiveManager.objectiveTotal}"; // Update to reflect the actual maximum objective count
            wallObjectiveText.text = $"Walls Open: {objectiveManager.completedWallObjectives}/{objectiveManager.wallObjectiveCount}";
        }

        private void UpdateObjectiveUIOnStart()
        {
            objectiveText.text = $"Objective Complete: {objectiveManager.completedObjectives}/{objectiveManager.objectiveTotal}";
            wallObjectiveText.text = $"Walls Open: {objectiveManager.completedWallObjectives}/{objectiveManager.wallObjectiveCount}";
        }
    }
}
