using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndTriggerScript : MonoBehaviour
{
    LevelManagerScript levelManagerScript;
    SaveScript saveScript;
    public static EndTriggerScript Instance;
    public GameObject levelFinishPanel;

    void Awake() {
        levelManagerScript = GetComponent<LevelManagerScript>();
        saveScript = GetComponent<SaveScript>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.tag.Equals("Player")) { return; }

        //stop player movement things
        collision.GetComponent<PlayerScript>().enabled = false;
        collision.GetComponent<PlayerInput>().enabled = false;
        collision.GetComponentInChildren<ArmScript>().enabled = false;
        collision.GetComponentInChildren<HookGunScript>().enabled = false;


        ClockManagerScript.continueClock = false;

        int levelNum = levelManagerScript.getCurrentLevelNumber();

        LevelSaveData lvlData = new LevelSaveData {levelID = levelNum, bestTime = ClockManagerScript.timer}; 
        saveScript.Save(lvlData);

        levelFinishPanel.SetActive(true);
        levelFinishPanel.GetComponent<LevelFinishPanelScript>().updatePB(saveScript.LoadBestTime(levelNum));
    }
}
