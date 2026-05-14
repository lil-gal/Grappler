using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BadgeInfoScript : MonoBehaviour
{
    public TMP_Text badgeName;
    public Image badgeImage;

    public BadgeImageScript ImgScript; 

    public void Change(Badge badge) {
        Change(badge.name);
    }
    public void Change(string badgeName) {
        this.badgeName.text = badgeName;
        badgeImage.sprite = ImgScript.GetBadgeImage(badgeName); 
    }
}
