using TMPro;
using UnityEngine;

public class ClockManagerScript : MonoBehaviour
{
    public static float timer;
    public static bool continueClock = false;

    public TMP_Text minSecsText;
    public TMP_Text msText;

    void Update ()
    {
        if (continueClock == true) {
            timer += Time.deltaTime;
        }       

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        int ms = Mathf.FloorToInt((timer % 1f) * 100f);

        string niceTime1 = string.Format("{0:0}:{1:00}", minutes, seconds);
        string niceTIme2 = string.Format("{0:00}",ms);
        minSecsText.text =  niceTime1;
        msText.text = niceTIme2;
    }

    
}
