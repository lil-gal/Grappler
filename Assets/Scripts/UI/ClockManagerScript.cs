using TMPro;
using UnityEngine;

public class ClockManagerScript : MonoBehaviour
{
    public static float timer;
    public static bool continueClock = false;

    public TMP_Text[] minSecsText;
    public TMP_Text[] msText;

    void Update ()
    {
        if (continueClock == true) {
            timer += Time.deltaTime;
        }



        string[] texts = formatTime(timer);

        foreach(TMP_Text t in minSecsText) {
            t.text =  texts[0];
        }
        foreach(TMP_Text t in msText) {
            t.text = texts[1];
        }
    }

    public static void Reset() {
        timer = 0;
        continueClock = false;
    }

    public static string[] formatTime(float time) {
        string[] strings = {"",""};

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int ms = Mathf.FloorToInt((time % 1f) * 100f);

        string niceTime1 = string.Format("{0:0}:{1:00}", minutes, seconds);
        string niceTime2 = string.Format("{0:00}",ms);
        
        strings[0] = niceTime1;
        strings[1] = niceTime2;

        return strings;
    }

    
}
