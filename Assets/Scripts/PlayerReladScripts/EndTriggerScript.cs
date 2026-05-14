using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndTriggerScript : MonoBehaviour
{
    LevelManagerScript levelManagerScript;
    SaveScript saveScript;
    public static EndTriggerScript Instance;
    public GameObject levelFinishPanel;

    public BadgeInfoScript badgeInfo;

    private Badge Bronze;
    private Badge Silver;
    private Badge Gold;
    private Badge DevBeater;

    public ReadableTime silverTime;
    public ReadableTime goldTime;
    public ReadableTime devTime;



    void Awake() {
        levelManagerScript = GetComponent<LevelManagerScript>();
        saveScript = GetComponent<SaveScript>();

        Bronze = Badge.bronze;
        Silver = Badge.silver;
        Gold = Badge.gold;
        DevBeater = Badge.devBeater;

        // BRONZE = NaN (will always get it)
        Silver.timeNeeded = silverTime.ToFloat();
        Gold.timeNeeded = goldTime.ToFloat();
        DevBeater.timeNeeded = devTime.ToFloat();


        
        
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
        LevelSaveData lvlData = new LevelSaveData {levelID = levelNum, bestTime = ClockManagerScript.timer, badgeName = GetBadge().name}; 
        saveScript.Save(lvlData);

        levelFinishPanel.SetActive(true);
        LevelFinishPanelScript finishPanelScript = levelFinishPanel.GetComponent<LevelFinishPanelScript>();
        finishPanelScript.updatePB(saveScript.LoadBestTime(levelNum));
        badgeInfo.Change(GetBadge().name);                      // current badge
        //badgeInfo.Change(saveScript.LoadBadgename(levelNum)); // best badge

    }

    public Badge GetBadge() {
        Badge[] badges = {DevBeater, Gold, Silver};
        
        float time = ClockManagerScript.timer;
        
        foreach(Badge badge in badges) {
            if(badge.IsTimeQualified(time)) return badge;
        }
        return Bronze;
    }
}
