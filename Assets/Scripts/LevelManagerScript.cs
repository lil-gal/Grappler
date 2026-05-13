using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManagerScript : MonoBehaviour
{
    // ==== SUPPORTING ===
    static bool SceneExists(string sceneName) {
        int count = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < count; i++) {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = Path.GetFileNameWithoutExtension(path);

            if (name == sceneName)
                return true;
        }
        return false;
    }

    IEnumerator LoadNext(string nextLevel) {
        yield return SceneManager.LoadSceneAsync(nextLevel);
    }

    // ==== METHODS ====

    public void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ClockManagerScript.Reset();
    }

    public static void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ClockManagerScript.Reset();
    }




    public void NextLevel() { 
        LoadLevel( getCurrentLevelNumber() );
    }


    public string getCurrentLevelName() {
        return SceneManager.GetActiveScene().name;
    }
    public int getLevelNumber(string level) {
        return Convert.ToInt32(level.Replace("Level", "")) + 1;
    }
    public int getCurrentLevelNumber() {
        return getLevelNumber( getCurrentLevelName() );
    }

    public void LoadLevel(int levelNumber) {
        string nextLevel = $"Level{levelNumber}";
        LoadLevel(nextLevel);
        
    }
    public void LoadLevel(string nextLevel) {
        ClockManagerScript.Reset();
        if (SceneExists(nextLevel)) {
            StartCoroutine(LoadNext(nextLevel));
        } else {
            StartCoroutine(LoadNext("Menu"));
        }

    }
    public void LoadMenu() {
        StartCoroutine(LoadNext("Menu"));

    }

    public static int levelCount() {
        int i = 0;
        while (true) {
            string levelName = $"Level{i}";
            if (!SceneExists(levelName)) {
                return i;
            }
            i++;
        }
    }



    
}