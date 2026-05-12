using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    private string saveLocation;
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void Save(SaveData data) {
        if(data == null) {
            Save();
            return;
        }
        File.WriteAllText(saveLocation, JsonUtility.ToJson(data));
        
    }

    public void Save() {
        SaveData data = Load();
        if(data == null){ data = new SaveData(); data.levels = new List<LevelSaveData>(); }
        for(int i = 0; i < LevelManagerScript.levelCount(); i++) {
            if(i >= data.levels.Count) {
                data.levels.Add(new LevelSaveData {levelID = i, bestTime = -1});
            }
        }
        Save(data);
    }

    public void Save(LevelSaveData level) {
        SaveData data = Load();
        if(data == null) { return; }
        int index = data.levels.FindIndex(l => l.levelID == level.levelID);
        if (index >= 0)
            data.levels[index] = level;
        else
            data.levels.Add(level);
        Save(data);
    }

    public SaveData Load() {
        if (File.Exists(saveLocation)) {
            return JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
        }
        else return null;
    }

    public LevelSaveData Load(int levelNum) {
        SaveData saveData = Load();
        if(saveData == null) { return null; }
        
        foreach(LevelSaveData level in saveData.levels) {
            if(level.levelID == levelNum)
            return saveData.levels[levelNum];
        }
        return null;
    }
    public float LoadBestTime(int levelNum) {
        LevelSaveData level = Load(levelNum);
        if(level == null) { return -1f; }
        
        return level.bestTime;
        
    }
}
