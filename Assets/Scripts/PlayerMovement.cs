using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float dashForce = 20f;
    private int jumpAmount = 2;
    private int jumpCount = 0;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0f);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)

        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)

        {
            Dash();
        }
    }

    private void Jump()
    {
        if(jumpCount < 2) 
        {
            Debug.Log("Performing jump!");
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }
        else if(jumpCount == 2)
        {
            isGrounded = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void Dash()
    {
        Debug.Log("Performing dash!");
        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        rb.AddForce(dashDirection * dashForce / dashDuration, ForceMode2D.Impulse);
        canDash = false;
        Invoke(nameof(EndDash), dashDuration);
        Invoke(nameof(EnableDash), dashCooldown);
    }

    private void EndDash()
    {
        //rb.velocity = Vector2.zero;
        canDash = false;
    }

    private void EnableDash()
    {
        canDash = true;
    }

}
