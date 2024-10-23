using UnityEngine;
using TMPro;
using WallThrough.Gameplay;
using WallThrough.Gameplay.Pawn;
using UnityEngine.UI;

namespace WallThrough.UI
{
    /// <summary>
    /// Manages the user interface for displaying objectives.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        // References
        [SerializeField] private ObjectiveManager objectiveManager;

        // ObjectiveUI
        [SerializeField] private TMP_Text objectiveText; // Text component for displaying objectives
        [SerializeField] private TMP_Text wallObjectiveText; // Text component specifically for wall objectives

        // PlayerUI
        [SerializeField] private Image[] hearts;
        [SerializeField] private Image[] breathBubbles;
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite emptyHeart;
        [SerializeField] private Sprite breath;

        private void Start()
        {
            UpdateObjectiveUIOnStart();
        }

        private void OnEnable()
        {
            Objective.OnObjectiveCompleted += UpdateObjectiveUI; // Subscribe to objective completion events
            PlayerStats.OnHealthChange += UpdatePlayerStatsHealthUI; //The name Player does not exist in current context
            PlayerStats.OnBreathChange += UpdatePlayerStatsBreathUI; //The name Player does not exist in current context
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

        private void UpdatePlayerStatsHealthUI(int health)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].sprite = (i < health) ? fullHeart : emptyHeart;
            }
        }

        private void UpdatePlayerStatsBreathUI(int breath)
        {
            for (int i = 0; i < breathBubbles.Length; i++)
            {
                // Enable or disable breath bubbles based on current breath value
                breathBubbles[i].gameObject.SetActive(i < breath);
            }
        }
    }
}
