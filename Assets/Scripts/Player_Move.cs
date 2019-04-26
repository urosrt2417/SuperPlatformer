using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float playerSpeed = 10;
    public float playerJumpPower = 500;
    public float slowFactor = 2;
    public bool isGrounded;

    private float moveX;
    private float minPlayerSpeed, maxPlayerSpeed;
    private bool facingRight = false;

    public Animator animator;

    void Start()
    {
        minPlayerSpeed = playerSpeed / slowFactor;
        maxPlayerSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        // Controls
        moveX = Input.GetAxis("Horizontal") * playerSpeed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Animations
        animator.SetFloat("Speed", Mathf.Abs(moveX));

        // Crouching
        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("IsCrouching", true);
        } else if (Input.GetButtonUp("Crouch"))
        {
            animator.SetBool("IsCrouching", false);
        }

        // Player direction
        if (moveX < 0.0f && !facingRight)
        {
            FlipPlayer();
        } else if (moveX > 0.0f && facingRight)
        {
            FlipPlayer();
        }

        // Physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        // Jumping code
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
        playerSpeed = maxPlayerSpeed;
        animator.SetBool("IsJumping", true);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("IsJumping", false);
        
        if (collision.gameObject.tag == "ground")
        {
            if (playerSpeed < maxPlayerSpeed)
            {
                playerSpeed *= slowFactor;
            }
            isGrounded = true;
        }
        else if (collision.gameObject.tag == "moss")
        {
            if (playerSpeed > minPlayerSpeed)
            {
                playerSpeed /= slowFactor;
            }
            isGrounded = true;
        }
    }
}
