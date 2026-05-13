using UnityEngine;

public class HookGunScript : MonoBehaviour
{

    public GameObject hookPrefab;
    [HideInInspector] public HookScript hookInstance; 
    SpriteRenderer ren;
    public Sprite Loaded;
    public Sprite Unloaded;

    void Start() {
        ren = transform.parent.GetComponent<SpriteRenderer>();
        ren.sprite = Loaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire() {
        if(hookInstance == null) { // didnt fire yet
            GameObject hook = Instantiate(hookPrefab,transform.position, transform.rotation, transform);
            hookInstance = hook.GetComponent<HookScript>();
            ren.sprite = Unloaded;
        }
        else {                   // fired alr
            hookInstance.Retract();
        }
    }

    public bool TryToGetOff() {
        if(hookInstance != null) { // didnt fire yet
            hookInstance.Retract();
            return true;
        }
        return false;
    }

    public void OnReturn() {
        hookInstance = null;  // if null, can fire, else not
        ren.sprite = Loaded;
    }
}
