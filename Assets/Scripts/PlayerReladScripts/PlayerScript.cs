using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float acceleration;
    public float maxMomentum;
    public float deceleration;
    public float jumpForce = 7.2f;
    public float wallJumpForce = 4.5f;

    public float jumpBufferTime = 0.15f;
    float jumpBufferCounter; 

    public float wallJumpBufferTime = 0.15f;
    float wallJumpBufferCounter;

    public float onWallTime = 2f;
    float onWallCounter;
    public float gravityScale = 2.5f;
    public float slideGravity = 1.5f;

    public float deathZoneBelow = 0f;
    

    private Rigidbody2D rb;

    public LayerMask walkables;
    public LayerMask wallJumpables;
    public GameObject groundCheck;
    HookGunScript hookGunScript;
    CinemachinePositionComposer CMCam;


    private Vector2 moveInput;
    Vector2 momentum;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        hookGunScript = GetComponentInChildren<HookGunScript>();
        CMCam = GameObject.FindWithTag("CinemaChineCamera").GetComponent<CinemachinePositionComposer>();
        rb.gravityScale = gravityScale;
    }

    bool moved;


    bool touchingGround;
    bool touchingWall;
    bool canWallJump;
    void FixedUpdate() {
        touchingGround = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, .2f, walkables);
        

        Vector2 movingSide = rb.linearVelocityX < 0 ? Vector2.left : Vector2.right;

        touchingWall = Physics2D.Raycast(transform.position, movingSide, transform.localScale.x/2 + .15f, wallJumpables);
        
        
        
    }

    public void StartSwing() {
        disableMomentumLine = true;
    }
    public void StopSwing() {
        disableMomentumLine = false;
    }

    
    bool disableMomentumLine;
    void Update()
    {

        if(transform.position.y < deathZoneBelow) {
            LevelManagerScript.Reset();
        }
        float dt = Time.deltaTime;
        
        if(moveInput.magnitude == 0) { //not moving            
            momentum = Vector2.MoveTowards(momentum, Vector2.zero, deceleration * Time.deltaTime );

        }else{
            float multiplier = 1f;
            if (!touchingGround) {
                multiplier = 0.25f;
            }


            // If input is opposite to current momentum, decelerate faster
            if (Vector2.Dot(moveInput, momentum) < 0){
                momentum = Vector2.MoveTowards(momentum, Vector2.zero, deceleration * 1.5f * dt);
            }

            momentum += moveInput * acceleration * dt * multiplier;
            momentum = Vector2.ClampMagnitude(momentum, maxMomentum);
        }

        //swinging mess
        if (!disableMomentumLine) {
            rb.linearVelocityX = momentum.x;
        }
        else {
            Vector2 vel = rb.linearVelocity;
            
            vel.x += moveInput.x * dt * acceleration / 2; 
            vel = Vector2.ClampMagnitude(vel, maxMomentum);
            
            rb.linearVelocity = vel;

            momentum.x = vel.x; // sync for smooth handoff when hook releases
        }

        if (!jumpReleased) {
            jumpHoldTimer += dt;
            if(jumpHoldTimer > 1) {
                jumpHoldTimer = 1;
            }
        }

        //JUMPING  
        if (!touchingGround) { 
            jumpBufferCounter = jumpBufferTime;
        }
        else {
            canWallJump = true;
        }

        if (jumpBufferCounter > 0 ){
            jumpBufferCounter -= dt;
        }

        if (jumpBufferCounter > 0 && touchingGround && wantToJump){
            Jump();
        } 

        //camera options
        if(rb.linearVelocityY > 0) { //going up
            CMCam.Lookahead.IgnoreY = true;
        }
        else {
            CMCam.Lookahead.IgnoreY = false;
        }


        //wall slide/jump

        if(touchingWall) {
            if(onWallCounter <= 0) {
                //SLIDE
                rb.gravityScale = slideGravity;
                momentum.x = 0;
            }
            else {
                onWallCounter -= dt;
            }
        }
        else {
            onWallCounter = onWallTime;
            rb.gravityScale = gravityScale;
        }


        if (!touchingWall) { //if not touching a wall - reset 
            jumpBufferCounter = jumpBufferTime;
        }

        if (wallJumpBufferCounter > 0 ){
            wallJumpBufferCounter -= dt;
        }
        if (wallJumpBufferCounter > 0 && canWallJump && touchingWall && wantToJump) {
            canWallJump = false;
            momentum.x *= -0.90f;
            rb.linearVelocityY = wallJumpForce;
            wallJumpBufferCounter = 0;
        }


    }

    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();

        tryStartClock();
    }

    void tryStartClock() {
        if (!moved) {
            moved = true;
            ClockManagerScript.continueClock = true;
        }
    }

    private void Jump() {
        rb.linearVelocityY = jumpForce;
        jumpBufferCounter = 0;
    }

    bool wantToJump;
    float jumpHoldTimer;
    bool jumpReleased;
    public void OnJump(InputAction.CallbackContext context) {
        if (!touchingGround && context.performed) { //if they press jump in the air
            if (hookGunScript.TryToGetOff()) { // jump off the swing if swinging + {} if true
                rb.linearVelocity *= 1.1f; //slight boost
            } 
        }

        if (context.performed){
            jumpBufferCounter = jumpBufferTime;
            wallJumpBufferCounter = wallJumpBufferTime;
            wantToJump = true;
        }
        if (context.canceled) {
            wantToJump = false;
            jumpReleased = true;
        }

        tryStartClock();
        
    }

    public void OnFire(InputAction.CallbackContext context) {
        if(!context.started) { return; }
        hookGunScript.Fire();
        canWallJump = true; //might delete

        tryStartClock();
    }
}
