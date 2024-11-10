using System.Collections;
using UnityEngine;
using WallThrough.Gameplay.Interactable;
using WallThrough.Core;
using UnityEngine.SceneManagement;
using System.IO;

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
        [SerializeField] private GameObject portalVFX;

        [SerializeField] private string sceneToLoadName;

        private void Start()
        {
            // Find and assign the ObjectiveManager in the scene
            objectiveManager = FindObjectOfType<ObjectiveManager>();

            if (!winText)
            {
                try
                {
                    winText = GameObject.Find("GameplayCanvas").transform.Find("YouWinText").gameObject;
                }
                catch
                {
                    Debug.LogWarning("Could not find the YouWinText gameobject, is the name changed?");
                }
            }

            if (!adviceText)
            {
                try
                {
                    adviceText = GameObject.Find("GameplayCanvas").transform.Find("AdviceText").gameObject;
                }
                catch
                {
                    Debug.LogWarning("Could not find the AdviceText gameobject, is the name changed?");
                }
            }
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

        private void Update()
        {
            if (objectiveManager.CheckObjectives())
            {
                portalVFX.SetActive(true);
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

            // Get the current scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Get the path of the next scene by build index
            string nextScenePath = SceneUtility.GetScenePathByBuildIndex(currentSceneIndex + 1);

            // Extract the scene name from the path
            string nextSceneName = Path.GetFileNameWithoutExtension(nextScenePath);

            // Load the next scene by name
            LevelManager.Instance.LoadScene(nextSceneName);
        }
    }
}
