using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private TrailRenderer tr;
    [SerializeField] private Animator anim;

    #region BASIC MOVEMENT

    [SerializeField] private bool isGrounded = true;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float checkRadius;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private int extraJumpsValue = 1;
    private int extraJumps;
    private float input;
    private bool isFacingRight = true;

    #endregion

    #region DASH SETTINGS
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 5f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    #endregion

    #region WALLSLIDE & JUMP

    private bool isTouchingWall;
    private bool isWallSliding;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallSlidingSpeed;
    private bool wallJumping;
    [SerializeField] private float xWallForce;
    [SerializeField] private float yWallForce;
    [SerializeField] private float wallJumpTime;


    #endregion

    #region OTHER

    private int invokeCount;

    #endregion


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        anim = GetComponent<Animator>();
        //InvokeRepeating("InvokeFunction", 0f, 0.5f);
    }


    private void Update()
    {
        if(isDashing)
        {
            return;
        }

        input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);

        if(input > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(input < 0 && isFacingRight) 
        {
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(isGrounded)
        {
            extraJumps = extraJumpsValue;
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);

        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;

        }

        if(rb.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }

        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }

        if(Mathf.Abs(rb.velocity.x) > 0)
        {
            anim.SetFloat("speed", 1);
        }
        else
        {
            anim.SetFloat("speed", -1);

        }




        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
            StartCoroutine(Dash());
        }

        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsGround);

        if (isTouchingWall && isGrounded && input != 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
            
        if(isWallSliding)
        {   
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if(Input.GetKeyDown(KeyCode.Space) && isWallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if(wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -input,yWallForce);
        }

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    private void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    private void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        isFacingRight =! isFacingRight;
    }

    private void InvokeFunction()
    {
        Debug.Log(rb.velocity.y);
        invokeCount++;

        if (invokeCount >= 500)
        {
            CancelInvoke("InvokeFunction");
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.AddForce(dashDirection * dashingPower, ForceMode2D.Impulse);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}
