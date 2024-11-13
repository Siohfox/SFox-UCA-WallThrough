using UnityEngine;
using TMPro;
using WallThrough.Gameplay;
using WallThrough.Gameplay.Pawn;
using UnityEngine.UI;
using System;
using System.Collections;

namespace WallThrough.UI
{
    /// <summary>
    /// Manages the user interface for displaying objectives.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ObjectiveManager objectiveManager;

        [Header("ObjectiveUI")]
        [SerializeField] private TMP_Text objectiveText; // Text component for displaying objectives
        [SerializeField] private TMP_Text wallObjectiveText; // Text component specifically for wall objectives

        [Header("PlayerUI")]
        [SerializeField] private Image[] hearts;
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite emptyHeart;

        // Other UI
        [SerializeField] private GameObject deathUI;
        [SerializeField] private TMP_Text countdownTimerText;

        private void Start()
        {
            UpdateObjectiveUIOnStart();
        }

        private void OnEnable()
        {
            Objective.OnObjectiveCompleted += UpdateObjectiveUI; // Subscribe to objective completion events
            PlayerStats.OnHealthChange += UpdatePlayerStatsHealthUI; //The name Player does not exist in current context
            PlayerStats.OnPlayerDeath += DisplayPlayerDeathUI;
        }

        private void OnDisable()
        {
            Objective.OnObjectiveCompleted -= UpdateObjectiveUI; // Subscribe to objective completion events
            PlayerStats.OnHealthChange -= UpdatePlayerStatsHealthUI; //The name Player does not exist in current context
            PlayerStats.OnPlayerDeath -= DisplayPlayerDeathUI;
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
                if(hearts[i] != null)
                {
                    hearts[i].sprite = (i < health) ? fullHeart : emptyHeart;
                }               
            }
        }

        private void DisplayPlayerDeathUI()
        {
            deathUI.SetActive(true);
        }

        public void DisplayTextGameObject(GameObject textObject, bool flashText, float timeToWait)
        {
            if (flashText)
            {
                StartCoroutine(FlashText(textObject, timeToWait));
            }
            else
            {
                textObject.SetActive(true);
            }
        }

        public static IEnumerator FlashText(GameObject textGameobject, float timeToWait)
        {
            textGameobject.SetActive(true);
            yield return new WaitForSeconds(timeToWait);
            textGameobject.SetActive(false);
        }

        public void UpdateTimer(float timer)
        {
            countdownTimerText.text = Mathf.CeilToInt(timer).ToString();
        }
    }
}
