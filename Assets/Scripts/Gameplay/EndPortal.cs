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
        [SerializeField] private GameObject winText; // Text displayed when objectives are complete
        [SerializeField] private GameObject adviceText; // Text displayed when objectives are incomplete
        [SerializeField] private GameObject portalVFX; // Visual effect for the portal
        private ObjectiveManager objectiveManager; // Reference to the ObjectiveManager

        private void Start()
        {
            AssignUndeclaredVariables();
        }

        private void AssignUndeclaredVariables()
        {
            // Assign ObjectiveManager from the scene
            objectiveManager = FindObjectOfType<ObjectiveManager>();
            if (objectiveManager == null)
                Debug.LogError("ObjectiveManager not found in the scene.");

            // Assign winText if not already set
            if (winText == null)
            {
                var canvas = GameObject.Find("GameplayCanvas");
                if (canvas != null)
                {
                    var winTextObj = canvas.transform.Find("YouWinText");
                    if (winTextObj != null)
                    {
                        winText = winTextObj.gameObject;
                    }
                    else
                    {
                        Debug.LogWarning("Could not find the 'YouWinText' GameObject. Is the name changed?");
                    }
                }
                else
                {
                    Debug.LogWarning("GameplayCanvas not found in the scene.");
                }
            }

            // Assign adviceText if not already set
            if (adviceText == null)
            {
                var canvas = GameObject.Find("GameplayCanvas");
                if (canvas != null)
                {
                    var adviceTextObj = canvas.transform.Find("AdviceText");
                    if (adviceTextObj != null)
                    {
                        adviceText = adviceTextObj.gameObject;
                    }
                    else
                    {
                        Debug.LogWarning("Could not find the 'AdviceText' GameObject. Is the name changed?");
                    }
                }
                else
                {
                    Debug.LogWarning("GameplayCanvas not found in the scene.");
                }
            }
        }

        /// <summary>
        /// Starts the interaction with the end portal.
        /// </summary>
        public void InteractionStart()
        {
            if (objectiveManager != null && objectiveManager.CheckObjectives())
                StartCoroutine(WaitForEnd());
            else
                StartCoroutine(GiveAdvice());
        }

        private void Update()
        {
            // Activate portal VFX if objectives are complete
            if (objectiveManager != null && objectiveManager.CheckObjectives() && portalVFX != null)
                portalVFX.SetActive(true);
        }

        /// <summary>
        /// Ends the interaction with the end portal. Currently not implemented.
        /// </summary>
        public void InteractionEnd()
        {
            // Placeholder for future functionality
        }

        /// <summary>
        /// Displays advice text for a brief period.
        /// </summary>
        private IEnumerator GiveAdvice()
        {
            if (adviceText != null)
            {
                adviceText.SetActive(true);
                yield return new WaitForSeconds(2f);
                adviceText.SetActive(false);
            }
            else
            {
                Debug.LogWarning("AdviceText is not assigned.");
            }
        }

        /// <summary>
        /// Displays the win text and transitions to the next scene or menu.
        /// </summary>
        private IEnumerator WaitForEnd()
        {
            if (winText != null)
                winText.SetActive(true);
            else
                Debug.LogWarning("WinText is not assigned.");

            yield return new WaitForSeconds(2f);

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Determine if there's another scene or return to the menu
            if (currentSceneIndex + 1 >= SceneManager.sceneCountInBuildSettings)
            {
                LevelManager.Instance.LoadScene("Menu");
            }
            else
            {
                string nextScenePath = SceneUtility.GetScenePathByBuildIndex(currentSceneIndex + 1);
                string nextSceneName = Path.GetFileNameWithoutExtension(nextScenePath);
                LevelManager.Instance.LoadScene(nextSceneName);
            }
        }
    }
}
