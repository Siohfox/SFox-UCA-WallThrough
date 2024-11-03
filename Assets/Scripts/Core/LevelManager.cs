using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace WallThrough.Core
{
    public class LevelManager : MonoBehaviour
    {
        // Singleton
        public static LevelManager Instance { get; private set; }

        [SerializeField] private Image loadingBar;
        [SerializeField] private TMP_Text progressText;
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Animator transition;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        // Loads a specific scene via build index number
        public void LoadScene(string sceneName)
        {
            StartCoroutine(InitLoading(sceneName));
        }

        private IEnumerator LoadAsynchronously(string sceneName)
        {
            // Prepare the loading bar
            loadingBar.fillAmount = 0;

            // Start loading the scene asynchronously
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneUtility.GetBuildIndexByScenePath(sceneName), LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false; // Prevent automatic activation of the scene

            // While the scene is loading, update the loading bar and progress text
            while (!asyncOperation.isDone)
            {
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                progressText.text = (Mathf.Round(progress * 100f) + "%");
                loadingBar.fillAmount = progress;

                // Check if loading is complete
                if (asyncOperation.progress >= 0.9f)
                {
                    // Optionally, you can break the loop here to do additional things before transition
                    break;
                }

                yield return null; // Wait for the next frame
            }

            // At this point, loading is done, and we can return to the caller
        }

        private IEnumerator InitLoading(string sceneName)
        {
            // Activate the loading screen
            if (loadingScreen) loadingScreen.SetActive(true);

            // Start loading the scene asynchronously
            yield return StartCoroutine(LoadAsynchronously(sceneName));

            // At this point, loading is complete
            transition.SetTrigger("Start");
            Debug.Log("Starting trigger");

            // Wait for the transition animation to complete
            yield return new WaitForSeconds(1f);

            // Finally, activate the loaded scene
            SceneManager.LoadScene(SceneUtility.GetBuildIndexByScenePath(sceneName));
        }

        // Reloads the current scene
        public void ReloadCurrentScene()
        {
            StartCoroutine(InitLoading(SceneManager.GetActiveScene().name));
        }

        // Quits the game
        public void QuitGame()
        {
            Application.Quit();
        }

        // Loads the first scene (menu)
        public void LoadMenu()
        {
            StartCoroutine(InitLoading(SceneUtility.GetScenePathByBuildIndex(0)));
        }
    }
}
