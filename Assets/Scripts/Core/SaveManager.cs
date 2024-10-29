using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace WallThrough.Core
{
    [System.Serializable]
    public class SaveData
    {
        public int score;
        public int currentLevel;
        public List<int> unlockedLevels;
    }

    [System.Serializable]
    public class OptionsData
    {
        public float volume;
        public float sfxVolume;
        public float musicVolume;
        public int quality;
        public bool fullscreen;
        public int resolutionIndex;
    }

    public class SaveManager : MonoBehaviour
    {
        private string saveFilePath;
        private string optionsFilePath;

        private void Awake()
        {
            saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
            optionsFilePath = Path.Combine(Application.persistentDataPath, "optionsfile.json");
        }

        public void SaveGame(GameManager gameManager)
        {
            SaveData data = new()
            {
                score = gameManager.score,
                currentLevel = gameManager.currentLevel,
                unlockedLevels = gameManager.unlockedLevels
            };

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(saveFilePath, json);
            Debug.Log("Game Saved");
        }

        public void SaveOptionsData(OptionsData optionsData)
        {
            string json = JsonUtility.ToJson(optionsData);
            File.WriteAllText(optionsFilePath, json);
        }

        public OptionsData LoadOptionsData()
        {
            if (File.Exists(optionsFilePath))
            {
                string json = File.ReadAllText(optionsFilePath);
                OptionsData optionsData = JsonUtility.FromJson<OptionsData>(json);
                return optionsData;
            }
            else
            {
                Debug.LogWarning("Options file not found, loading defaults");
                return new OptionsData(); // Return default options if no file exists
            }
        }

        public void LoadGame(GameManager gameManager)
        {
            if(File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                gameManager.score = data.score;
                gameManager.currentLevel = data.currentLevel;
                gameManager.unlockedLevels = data.unlockedLevels;

                Debug.Log("Game Loaded");
            }
            else
            {
                Debug.LogWarning("Save file not found");
            }
        }
    }
}
