using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    public GameObject[] panels;

    public void pickOne(GameObject choosePanel) {
        foreach(GameObject panel in panels) {
            panel.SetActive(false);
        }
        choosePanel.SetActive(true);
    }
}
