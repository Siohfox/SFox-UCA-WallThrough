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

        // Loads the next scene
        public void LoadNextScene()
        {
            string sceneName = SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
            if(loadingScreen) loadingScreen.SetActive(true);
            StartCoroutine(LoadAsynchronously(sceneName));
        }

        // Loads a specific scene via build index number
        public void LoadScene(string sceneName)
        {
            if (loadingScreen) loadingScreen.SetActive(true);
            StartCoroutine(LoadAsynchronously(sceneName));
        }

        private IEnumerator LoadAsynchronously(string sceneName)
        {
            loadingBar.fillAmount = 0;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneUtility.GetBuildIndexByScenePath(sceneName), LoadSceneMode.Single);

            //loadingScreen.SetActive(true);

            while (!asyncOperation.isDone)
            {
                float progress = Mathf.Clamp01(asyncOperation.progress / .9f);
                progressText.text = (Mathf.Round(progress * 100f) + "%");
                loadingBar.fillAmount = asyncOperation.progress;
                
                yield return new WaitForEndOfFrame();
            }
        }

        // Loads the first scene (menu)
        public void LoadMenu()
        {
            SceneManager.LoadSceneAsync(0);
        }

        public void ReloadCurrentScene()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
        // Quits the game
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}