using UnityEngine;

using UnityEngine.InputSystem;

public class ArmScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0f;

        Vector2 direction = mouseWorld - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float parentAngle = transform.parent ? transform.parent.eulerAngles.z : 0f;

        transform.localEulerAngles = new Vector3(0, 0, angle - parentAngle);
        
    }
}
