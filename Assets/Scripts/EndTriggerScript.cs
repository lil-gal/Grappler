using UnityEngine;

public class EndTriggerScript : MonoBehaviour
{
    LevelManagerScript levelManager;
    void Start() { //will move this to the UI thing
        levelManager = GetComponent<LevelManagerScript>();
    }
    void OnTriggerEnter2D(Collider2D collision) {
        ClockManagerScript.continueClock = false;
        
        // call ui for finishing level
        
        levelManager.NextLevel();
    }
}
