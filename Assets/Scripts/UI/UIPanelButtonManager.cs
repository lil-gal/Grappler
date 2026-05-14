using UnityEngine;
using UnityEngine.UI;

public class UIPanelButtonManager : MonoBehaviour
{
    public Button button;

    public void onOpen() {
        button.Select();
    }
}
