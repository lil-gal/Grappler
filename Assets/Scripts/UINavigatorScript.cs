using UnityEngine;

public class UINavigatorScript : MonoBehaviour
{
    public GameObject disableThis;
    public GameObject enableThis;

    public void Change() {
        enableThis.SetActive(true);
        disableThis.SetActive(false);
    }
}
