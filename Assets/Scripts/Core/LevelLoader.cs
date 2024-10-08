using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    // Singleton
    public static LevelLoader Instance { get; private set; }

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

    private void Start()
    {
        Time.timeScale = 1;
    }

    // Loads the next scene
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Loads a specific scene via build index number
    public void LoadScene(int sceneNumber)
    {
        // Destroy the menu music player before loading a new scene

        StartCoroutine(LoadAsynchronously(sceneNumber));
    }

    private IEnumerator LoadAsynchronously(int sceneNumber)
	{
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Single);

        //loadingScreen.SetActive(true);

        while (!operation.isDone)
		{
            //float progress = Mathf.Clamp01(operation.progress / .9f);

            //slider.value = progress;
            //progressText.text = (Mathf.Round(progress * 100f) + "%");

            yield return null;
		}
        if (GameObject.Find("MusicPlayer") != null)
        {
            Destroy(GameObject.Find("MusicPlayer"));
        }
    }

    // Loads the settings menu specifically
    public void LoadSettingsMenu()
    {
        // DontDestroyOnLoad(musicPlayer);
        SceneManager.LoadScene("SettingsMenu");
    }

    // Loads the first scene (menu)
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
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

    public void UpdateVariableHoldsOnLoad()
    {

    }
}
