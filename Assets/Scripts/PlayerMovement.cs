using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    public float movementSpeed;
    public bool isRunning;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Move();
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
            animator.SetBool("isRunning", true);
            isRunning = true;
            
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isRunning", false);
            isRunning = false;
        }
    }
}
