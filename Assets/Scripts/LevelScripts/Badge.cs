using UnityEngine;

public struct Badge
{
    public static readonly Badge empty = new Badge("", float.NaN);
    public static readonly Badge bronze = new Badge("Bronze Badge", float.NaN);
    public static readonly Badge silver = new Badge("Silver Badge", float.NaN);
    public static readonly Badge gold = new Badge("Gold Badge", float.NaN);
    public static readonly Badge devBeater = new Badge("Dev Badge", float.NaN);

    public string name;
    public float timeNeeded;

    public bool IsTimeQualified(float time) {
        if(time < 0) { return false; }

        if(float.IsNaN(timeNeeded)) {
            return true;
        }else if (time <= timeNeeded) {
            return true;
        }else {
            return false;
        }
    }

    public Badge(string name, float timeNeeded) {
        this.name = name;
        this.timeNeeded = timeNeeded;
    }
}
