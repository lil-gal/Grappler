using System;
using UnityEngine;

[Serializable]
public class ReadableTime
{
    public int minutes;
    public int seconds;
    public int ms;

    public float ToFloat(){
        return (minutes * 60f) + seconds + (ms / 1000f);
    }

    public static ReadableTime FromFloat(float time){
        return new ReadableTime {
            minutes = (int)(time / 60),
            seconds = (int)(time % 60),
            ms = (int)((time % 1f) * 1000f)
        };
    }

    public override string ToString() {
        if(minutes < 0 || seconds < 0 || ms < 0) {
            return "TBD";
        }
        return $"{minutes:0}:{seconds:00}.{ms:000}";
    }
}