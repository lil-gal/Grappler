using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float acceleration;
    public float maxMomentum;
    public float deceleration;
    

    private Rigidbody2D rb;


    private Vector2 moveInput;
    Vector2 momentum;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        momentum += moveInput * acceleration * Time.deltaTime;

        momentum = Vector2.ClampMagnitude(momentum, maxMomentum);
        
        //Debug.Log(momentum.x);
        
        if(moveInput.magnitude == 0) { //not moving
            momentum = Vector2.MoveTowards(momentum, Vector2.zero, deceleration * Time.deltaTime );
        }
        
        rb.linearVelocityX = momentum.x;
    }

    public void OnJump(InputAction.CallbackContext context) {
        
    }
    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }
}
