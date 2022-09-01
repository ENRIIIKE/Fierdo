using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Singleton
    //=====Singleton=====
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerController>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<PlayerController>();
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }
    #endregion

    [Header("Movement")]
    [HideInInspector] public PlayerHealth playerHealth;
    public Animator animator;
    public Rigidbody2D rb;

    public float movementSpeed;
    public float slow;
    public bool isRunning;
    public bool canMove;
    public bool canRotate;

    [Header("Jump")]
    public float jumpForce;
    public bool isJumping;
    public bool jumped;
    public bool canJump;

    [Header("Ground")]
    public bool grounded;
    public float circleRadius;
    public Transform groundCheckTransform;
    public LayerMask groundMask;

    private Vector2 knockback;

    private bool playerFacingRight;
    private bool playerFacingLeft;
    
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        canRotate = true;
    }
    void FixedUpdate()
    {
        GroundCheck();
        Move();
        Jump();

    }
    private void Move()
    {
        if (!canMove) return;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            if (moveHorizontal > 0.1f && canRotate)
            {
                transform.localScale = new Vector2(1f, 1f);
            }
            if (moveHorizontal < -0.1f && canRotate)
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
        else if (canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x / slow, rb.velocity.y);
            animator.SetBool("isRunning", false);
            isRunning = false;
        }
    }

    private void Jump()
    {
        if (!canMove && jumped) return;
        if (!canJump) return;

        float moveVertical = Input.GetAxisRaw("Vertical");

        if (moveVertical == 0f)
        {
            jumped = false;
            return;
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

        if (!canMove && collider != null && !playerHealth.playerDead)
        {
            if (rb.velocity.x > 1f) return;
            canMove = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheckTransform.position, circleRadius);
    }

    public void Knockback(Transform attacker, float knockbackStrength)
    {
        if (playerHealth.playerDead) return;

        canMove = false;

        rb.velocity = Vector2.zero;

        if (transform.position.x > attacker.position.x)
        {
            knockback = (Vector2.right * knockbackStrength) + Vector2.up * 4f;
        }
        else 
        {
            knockback = (Vector2.left * knockbackStrength) + Vector2.up * 4f;
        }

        rb.AddForce(knockback, ForceMode2D.Impulse);

    }
}
