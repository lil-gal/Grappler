using UnityEngine;

public class HookGunScript : MonoBehaviour
{
    bool canFire;

    public GameObject hookPrefab;
    void Start()
    {
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire() {
        Instantiate(hookPrefab);
    }
}
