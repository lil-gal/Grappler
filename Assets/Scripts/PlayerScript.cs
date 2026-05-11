using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float acceleration;
    public float maxMomentum;
    public float deceleration;
    public float jumpForce = 7.2f;

    public float jumpRayDistance = .2f;

    public float jumpBufferTime = 0.15f;
    float jumpBufferCounter; 
    

    private Rigidbody2D rb;

    public LayerMask walkables;
    public GameObject groundCheck;


    private Vector2 moveInput;
    Vector2 momentum;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }


    bool canJump;
    void FixedUpdate() {
        canJump = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, jumpRayDistance, walkables);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        
        if(moveInput.magnitude == 0) { //not moving            
            momentum = Vector2.MoveTowards(momentum, Vector2.zero, deceleration * Time.deltaTime );

        }else{


            // If input is opposite to current momentum, decelerate faster
            if (Vector2.Dot(moveInput, momentum) < 0){
                momentum = Vector2.MoveTowards(momentum, Vector2.zero, deceleration * 2f * dt);
            }

            momentum += moveInput * acceleration * dt;
            momentum = Vector2.ClampMagnitude(momentum, maxMomentum);
        }
        
        
        
        
        rb.linearVelocityX = momentum.x;

        if (!jumpReleased) {
            jumpHoldTimer += dt;
            if(jumpHoldTimer > 1) {
                jumpHoldTimer = 1;
            }
        }

        //JUMPING  
        if (!canJump) {
            jumpBufferCounter = jumpBufferTime;
        }

        if (jumpBufferCounter > 0 ){
            jumpBufferCounter -= dt;
        }

        if (jumpBufferCounter > 0 && canJump && wantToJump)
        {
            rb.linearVelocityY = jumpForce;
            jumpBufferCounter = 0;
        } 
    }

    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }

    bool wantToJump;
    float jumpHoldTimer;
    bool jumpReleased;
    public void OnJump(InputAction.CallbackContext context) {
        
        
        if (context.performed){
            jumpBufferCounter = jumpBufferTime;
            wantToJump = true;
        }
        if (context.canceled) {
            wantToJump = false;
            jumpReleased = true;
        }


        
        

    }

    public void OnFire(InputAction.CallbackContext context) {
        
    }
}
