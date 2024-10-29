using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public bool debugMode;
        public bool autoLoadSave;
        public enum GameState { MainMenu, Playing, Paused, GameOver }
        public GameState currentGameState;

        public int score;
        public int currentLevel;
        public List<int> unlockedLevels;

        [SerializeField] private SaveManager saveManager;

        private void Awake()
        {
            // Singleton Logic ///////
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            //////////////////////////

            if(!saveManager)
            {
                Debug.LogWarning("No save manager set in inspector");
            }
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            score = 0;
            currentLevel = 0;
            unlockedLevels = new List<int>();
            if (autoLoadSave)
            {
                LoadGame();
            }
        }

        private void Update()
        {
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Playing:
                    PauseGame(false);
                    break;
                case GameState.Paused:
                    PauseGame(true);
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }
        }

        private void PauseGame(bool paused)
        {
            if(paused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void ChangeGameState(GameState newState)
        {
            currentGameState = newState;
        }

        public void SetPlaying(bool playing)
        {
            if (playing)
            {
                currentGameState = GameState.Playing;
            }
        }

        public void SaveGame()
        {
            saveManager.SaveGame(this);
        }

        public void LoadGame()
        {
            saveManager.LoadGame(this);
        }

        public void IncreaseScore(int score)
        {
            this.score += score;
        }
    }
}

