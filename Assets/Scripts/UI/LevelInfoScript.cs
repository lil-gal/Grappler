using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoScript : MonoBehaviour
{
    public TMP_Text time;
    public Image badgeImage;

    public BadgeImageScript ImgScript; 
    public void Change(LevelSaveData data) {
        
        time.text = "PB - " + ReadableTime.FromFloat(data.bestTime).ToString();
        badgeImage.sprite = ImgScript.GetBadgeImage(data.badgeName); 
    }
}
