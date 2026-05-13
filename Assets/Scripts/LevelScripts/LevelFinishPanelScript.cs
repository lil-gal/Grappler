using TMPro;
using UnityEngine;

public class LevelFinishPanelScript : MonoBehaviour
{
    public TMP_Text minSecsText;
    public TMP_Text msText;
    public void updatePB(float pb) {
        if(pb < 0) {
            minSecsText.text = "TBD";
            msText.text = ""; 
            return;   
        }
        string[] times = ClockManagerScript.formatTime(pb);
        minSecsText.text = times[0];
        msText.text = times[1];
    }
}
