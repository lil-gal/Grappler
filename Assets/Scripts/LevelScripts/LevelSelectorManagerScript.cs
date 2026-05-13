using UnityEngine;

public class LevelSelectorManagerScript : MonoBehaviour
{
    public int levelsPerGrid = 6;
    public int atGridNum = 0;

    public void Change(int num) {
        atGridNum += num;
    }
}
