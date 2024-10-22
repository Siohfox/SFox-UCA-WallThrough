using System.Collections;
using UnityEngine;
using WallThrough.Gameplay.Interactable;
using WallThrough.Core;

namespace WallThrough.Gameplay
{
    /// <summary>
    /// Represents an end portal that can be interacted with to check objectives and display messages.
    /// </summary>
    public class EndPortal : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private GameObject winText; // Text to display when objectives are complete
        [SerializeField]
        private GameObject adviceText; // Text to provide advice when objectives are incomplete
        private ObjectiveManager objectiveManager; // Reference to the ObjectiveManager

        private void Start()
        {
            // Find and assign the ObjectiveManager in the scene
            objectiveManager = FindObjectOfType<ObjectiveManager>();
        }

        /// <summary>
        /// Starts the interaction with the end portal.
        /// </summary>
        public void InteractionStart()
        {
            if (objectiveManager.CheckObjectives())
            {
                StartCoroutine(WaitForEnd());
            }
            else
            {
                StartCoroutine(GiveAdvice()); // Provide advice if objectives are incomplete
            }
        }

        /// <summary>
        /// Ends the interaction with the end portal.
        /// </summary>
        public void InteractionEnd()
        {
            // Additional functionality can be added here if needed
        }

        /// <summary>
        /// Displays advice for a brief period.
        /// </summary>
        /// <returns>An enumerator for coroutine functionality.</returns>
        private IEnumerator GiveAdvice()
        {
            adviceText.SetActive(true); // Show advice text
            yield return new WaitForSeconds(2f); // Wait for 2 seconds
            adviceText.SetActive(false); // Hide advice text
        }

        private IEnumerator WaitForEnd()
        {
            winText.SetActive(true); // Show win text if objectives are completed (until portal object made)
            yield return new WaitForSeconds(2f);
            LevelManager.Instance.LoadMenu();
        }
    }
}
