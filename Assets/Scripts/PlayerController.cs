using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public Animator animator;
    public Rigidbody2D rb;

    public float movementSpeed;
    public float slow;
    public bool isRunning;

    [Header("Jump")]
    public float jumpForce;
    public bool isJumping;
    public bool jumped;

    [Header("Ground")]
    public bool grounded;
    public float circleRadius;
    public Transform groundCheckTransform;
    public LayerMask groundMask;

    void FixedUpdate()
    {
        GroundCheck();
        Move();
        Jump();
    }
    private void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            if (moveHorizontal > 0.1f)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
            if (moveHorizontal < -0.1f)
            {
                transform.localScale = new Vector2(-1f, 1f);
            }

            rb.velocity = new Vector2(moveHorizontal * movementSpeed, rb.velocity.y);
            isRunning = true;

            if (!animator.GetBool("isFalling"))
            {
                animator.SetBool("isRunning", true);
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x / slow, rb.velocity.y);
            animator.SetBool("isRunning", false);
            isRunning = false;
        }
    }

    private void Jump()
    {
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (moveVertical == 0f)
        {
            jumped = false;
        }

        if (jumped) return;

        if (moveVertical > 0.1f && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isRunning", false);
            jumped = true;
        }

    }
    private void GroundCheck()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckTransform.position, circleRadius, groundMask);

        if (collider != null)
        {
            grounded = true;
            isJumping = false;
            animator.SetBool("isFalling", false);
        }
        else
        {
            grounded = false;
            isJumping = true;
            animator.SetBool("isFalling", true);
            animator.SetBool("isRunning", false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckTransform.position, circleRadius);
    }
}
