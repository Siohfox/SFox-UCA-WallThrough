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
            ChangeGameState(GameState.MainMenu);
        }

        public void ChangeGameState(GameState newState)
        {
            currentGameState = newState;
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

