using UnityEngine;
using UnityEngine.UI;

public class BadgeImageScript : MonoBehaviour
{
    public Sprite ifNull;
    public Sprite bronzeImage;
    public Sprite silverImage;
    public Sprite goldImage;
    public Sprite devImage;


    public Sprite GetBadgeImage(Badge badge) {
        return GetBadgeImage(badge.name);
    }
    public Sprite GetBadgeImage(string badgeName){
        if (badgeName == Badge.bronze.name) return bronzeImage;
        if (badgeName == Badge.silver.name) return silverImage;
        if (badgeName == Badge.gold.name) return goldImage;
        if (badgeName == Badge.devBeater.name) return devImage;
        return ifNull;
    }
}