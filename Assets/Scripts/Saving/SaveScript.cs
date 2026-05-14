using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveScript : MonoBehaviour {
    private string saveLocation;

    void Start() {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        Save();
    }

    public void Save(SaveData data) {
        if (data == null) {
            return;
        }
        try {
            
            File.WriteAllText(saveLocation, JsonUtility.ToJson(data));
        } catch (IOException e) {
            Debug.LogError($"SaveScript: failed to write save file: {e.Message}");
        }
    }

    public void Save() {
        SaveData data = Load() ?? new SaveData { levels = new List<LevelSaveData>() };
        data.levels ??= new List<LevelSaveData>();
        
        for (int i = 0; i < LevelManagerScript.levelCount(); i++) {
            if (i >= data.levels.Count) {
                data.levels.Add(new LevelSaveData { levelID = i, bestTime = -1, badgeName = Badge.empty.name});
            }
        }

        Save(data);
    }

    public void Save(LevelSaveData level) {
        SaveData data = Load() ?? new SaveData { levels = new List<LevelSaveData>() };
        data.levels ??= new List<LevelSaveData>();

        int index = data.levels.FindIndex(l => l.levelID == level.levelID);
        if (index >= 0) {
            if (data.levels[index].bestTime < 0 || (level.bestTime >= 0 && level.bestTime < data.levels[index].bestTime)) {
                data.levels[index] = level;
            }
        } else {
            data.levels.Add(level);
        }

        Save(data);
    }

    public SaveData Load() {
        if (!File.Exists(saveLocation)) { return null; }

        string json;
        try {
            json = File.ReadAllText(saveLocation);
        } catch (IOException e) {
            Debug.LogError($"SaveScript: failed to read save file: {e.Message}");
            return null;
        }

        if (string.IsNullOrWhiteSpace(json)) { return null; }

        SaveData data;
        try {
            data = JsonUtility.FromJson<SaveData>(json);
        } catch (System.ArgumentException e) {
            Debug.LogError($"SaveScript: save file is corrupted: {e.Message}");
            return null;
        }

        if (data == null) { return null; }
        data.levels ??= new List<LevelSaveData>();
        return data;
    }

    public LevelSaveData Load(int levelNum) {
        SaveData saveData = Load();
        if (saveData == null) { return null; }

        foreach (LevelSaveData level in saveData.levels) {
            if (level.levelID == levelNum) {
                return level;
            }
        }

        return null;
    }

    public float LoadBestTime(int levelNum) {
        LevelSaveData level = Load(levelNum);
        if (level == null) { return -1f; }
        return level.bestTime;
    }
    public string LoadBadgename(int levelNum) {
        LevelSaveData level = Load(levelNum);
        if (level == null) { return Badge.empty.name; }
        return level.badgeName;
    }
}