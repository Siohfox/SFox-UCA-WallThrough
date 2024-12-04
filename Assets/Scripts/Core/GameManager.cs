using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WallThrough.Generation;

namespace WallThrough.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public enum GameState { MainMenu, Playing, Paused, GameOver }

        public bool debugMode;
        public bool autoLoadSave;
        public GameState currentGameState;
        public int score;
        public int currentLevel;
        public List<int> unlockedLevels = new();

        [SerializeField] private SaveManager saveManager;

        private List<RoomBehaviour> currentRooms = new(); // Holds the rooms for the current dungeon

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

            if (!saveManager)
            {
                try
                {
                    saveManager = GetComponent<SaveManager>();
                }
                catch
                {
                    Debug.LogWarning("No save manager set in inspector");
                }
            }
        }

        private void Start()
        {
            InitializeGame();

            Debug.Log(saveManager.LoadOptionsData().volume);
        }

        private void InitializeGame()
        {
            score = 0;
            currentLevel = 0;
            if (autoLoadSave) LoadGame();
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

        private void PauseGame(bool paused) => Time.timeScale = paused ? 0 : 1;
        public void ChangeGameState(GameState newState) => currentGameState = newState;
        public void SetPlaying(bool playing) { if (playing) currentGameState = GameState.Playing; }
        public void SaveGame() => saveManager.SaveGame(this);
        public void LoadGame() => saveManager.LoadGame(this);
        public void IncreaseScore(int score) => this.score += score;
        public void SetCurrentRooms(List<RoomBehaviour> rooms) => currentRooms = rooms;
        public List<RoomBehaviour> GetCurrentRooms() => currentRooms;

        public void DebugRoomList()
        {
            if (currentRooms == null || currentRooms.Count == 0)
            {
                Debug.Log("No rooms available in the current dungeon.");
                return;
            }

            Debug.Log("Rooms in the current dungeon:");
            for (int i = 0; i < currentRooms.Count; i++)
            {
                var room = currentRooms[i];
                Debug.Log($"{i + 1}: {room.name}");
            }
        }

    }
}
