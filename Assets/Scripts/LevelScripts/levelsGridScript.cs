using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class levelsGridScript : MonoBehaviour {

    public LevelSelectorManagerScript selectorManager;
    public LevelManagerScript levelManager;
    public GameObject levelButtonPrefab;
    public GameObject leftArrow;
    public GameObject rightArrow;

    SaveScript saveScript;
    
    void Start(){
        saveScript = GetComponent<SaveScript>();
        Refresh();
    }

    public void Refresh() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        
        int allLevelsNum = LevelManagerScript.levelCount();
        int startAtLevel = selectorManager.levelsPerGrid * selectorManager.atGridNum;
        int availableLevelsNum = Math.Clamp(allLevelsNum - startAtLevel,0, 6);
        
        for(int i = 0; i < availableLevelsNum; i++) {
            int lvlIndex = startAtLevel + i;
            GameObject btnObject = Instantiate(levelButtonPrefab, transform);
            Button btn = btnObject.GetComponent<Button>();

            LevelInfoScript timeAndBadge = btnObject.GetComponentInChildren<LevelInfoScript>();
            timeAndBadge.Change(saveScript.Load(lvlIndex));
            
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = $"Level {lvlIndex}";

            //Onclick
            btn.onClick.AddListener(() => {
                levelManager.LoadLevel(lvlIndex);
            }) ;
        }

        if(allLevelsNum > startAtLevel + availableLevelsNum) {
            rightArrow.SetActive(true);
        } else {
            rightArrow.SetActive(false);
        }
        if(selectorManager.atGridNum > 0) {
            leftArrow.SetActive(true);
        } else {
            leftArrow.SetActive(false);
        }
    }
}
