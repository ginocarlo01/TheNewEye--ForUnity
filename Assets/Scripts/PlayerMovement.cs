using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("JUMP")]
    [SerializeField] private float jumpForce;
    [SerializeField] private AudioSource jumpSFX;
    [SerializeField] private LayerMask layerJumpableGround;
    [SerializeField] private float fallMutiplier, fallVelocityMax;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpMultiplier;
    private Vector2 vecGravity;

    [Header("OTHERS")]
    [SerializeField] private float horizontalVelocity;
    [SerializeField] private float scaleYSpeed = 1f;
    [SerializeField] Signal pauseSignal;

    //components
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    //movements
    private float dirX;
    private bool canMove;

    //go up and down
    private bool floatingUp, floatingDown;
    private float scaleYDestiny;
    private bool upCheckIfIsGrounded = true;
    private float upDownModifier = 1;

    //jump modifier
    private bool pressingJump;
    private bool jumping;
    private float jumpCounter;

    //pause
    private bool pause;

    //instance
    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
    }

    private enum MovementState
    {
        idle,
        running,
        jump,
        fall
    }

    void Start()
    {
        InializeComponents();
        StartPlayerPosition();
    }

    private void InializeComponents()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);

        try
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
        catch (MissingComponentException e)
        {
            Debug.LogError($"There is a missing component: {e}");
        }
    }

    void Update()
    {
        if (canMove)
        {
            HandleInput();
            if (Input.GetButtonDown("Jump") && IsGrounded()) { StartJump(); }
            CheckJump();
            JumpModifier();

            HandleFall();

            HandleFloating();

            UpdateAnimation();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseSignal.Raise();
        }

    }

    private void JumpModifier()
    {
        if (jumping && pressingJump)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter >= jumpTime) pressingJump = false;

            float percentJump = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (percentJump > 0.6f)
            {
                currentJumpM = jumpMultiplier * (1 - percentJump);
            }

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }

    }

    private void HandleInput()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalVelocity * dirX, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            StartJump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            EndJump();
        }


    }

    private void EndJump()
    {
        pressingJump = false;
        jumpCounter = 0;

        if (jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.6f * rb.velocity.y);
        }
    }

    private void StartJump()
    {
        jumpSFX.Play();
        jumpCounter = 0;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        pressingJump = true;
    }

    private void HandleFloating()
    {
        if (floatingUp)
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, scaleYDestiny, scaleYSpeed), transform.localScale.z * Time.deltaTime);
            rb.velocity = new Vector2(0, jumpForce);
        }

        if (floatingDown)
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, scaleYDestiny, scaleYSpeed), transform.localScale.z * Time.deltaTime);
            rb.velocity = new Vector2(0, -jumpForce);
        }
    }

    private void CheckJump()
    {
        if (upDownModifier == 1)
        {
            if (rb.velocity.y > 0.4f * upDownModifier)
            {
                jumping = true;
            }
            else
            {
                jumping = false;
            }
        }
        else if (upDownModifier == -1)
        {
            if (rb.velocity.y < 0.4f * upDownModifier)
            {
                jumping = true;
            }
            else
            {
                jumping = false;
            }
        }
    }

    private void HandleFall()
    {
        if (!jumping) { rb.velocity -= vecGravity * fallMutiplier * Time.deltaTime; }

        if (rb.velocity.y < (-fallVelocityMax)) { rb.velocity = new Vector3(rb.velocity.x, -fallVelocityMax); }

    }

    public void HandlePauseInput()
    {
        pause = !pause;
        TogglePause(pause);
        
    }

    private void TogglePause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f; 
        }
        else
        {
            Time.timeScale = 1f; 
        }
    }

    private void UpdateAnimation()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            SetDirection("X", false);
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            SetDirection("X", true);
        }
        else
        {
            state = MovementState.idle;
        }

        if(jumping)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -5f && !IsGrounded())
        {
            state = MovementState.fall;
        }

        animator.SetInteger("state", ((int)state));
    }

    private void SetDirection(string directionText, bool state)
    {
        if(directionText == "X")
        {
            spriteRenderer.flipX = state;
        }
        if(directionText == "Y")
        {
            spriteRenderer.flipY = state;
        }
    }

    public void SetFloatingUp(bool floating)
    {
        this.floatingUp = floating;

        if (floatingUp)
        {
            SetMovement(false);
            scaleYDestiny = -1;
        }
        else
        {
            SetMovement(true);
            UpDown();
        }
    }

    public void SetFloatingDown(bool floating)
    {
        this.floatingDown = floating;

        if (floatingDown)
        {
            SetMovement(false);
            scaleYDestiny = 1;
            UpDown();
        }
        else
        {
            SetMovement(true);
            
        }
    }

    public void SetMovement(bool canMove)
    {
        this.canMove = canMove;
    }

    private bool IsGrounded()
    {
        if (upCheckIfIsGrounded)
        {
            return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, layerJumpableGround);
        }
        else
        {
            return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, .1f, layerJumpableGround);
        }

    }

    private void StartPlayerPosition()
    {
        if (!canMove)
        {
            if (JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].lastLocation != Vector3.zero)
            {
                transform.position = JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].lastLocation;
            }
            else
            {
                JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].lastLocation = transform.position;
            }

            canMove = true;
        }
    }

    private void UpDown()
    {
        rb.gravityScale *= -1;
        upCheckIfIsGrounded = !upCheckIfIsGrounded;
        jumpForce *= -1;
        fallMutiplier *= -1;
        upDownModifier *= -1;
        jumpMultiplier *= -1;
    }

    public void StopPlayerMovement()
    {
        rb.bodyType = RigidbodyType2D.Static;
        canMove = false;
    }
}
