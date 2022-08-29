using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBasic : MonoBehaviour
{
    [Header("General")]
    public Transform player;
    public Animator animator;
    public Rigidbody2D rb;
    public bool showGizmos = false;


    [Header("Stats")]
    public int health;
    public int damage;

    public float attackSpeed;
    public float movementSpeed;
    public float knockbackStrength;

    public bool canAttack = true;
    public bool canMove = true;
    public bool entityDead = false;

    [Header("Range")]
    public float attackRange;
    public bool playerInRange;

    [Header("Ground Check")]
    public Transform groundCheckTransform;
    public float circleRadius;
    public LayerMask groundMask;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    public void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < attackRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (canMove)
        {
            Move();
        }
        GroundCheck();
    }

    public void Attack()
    {
        if (!canAttack) return;
        canAttack = false;

        PlayerController.Instance.Knockback(transform, knockbackStrength);

        PlayerController.Instance.playerHealth.GetDamage(damage);

        StartCoroutine(AttackDelay());
    }

    public void GetDamage(int damage)
    {
        Debug.Log("Slime Damaged");
        
        if (damage > health)
        {
            damage = health;
            health = damage;
        }
        else
        {
            health = damage;
        }

        CheckHealth();
    }

    public void CheckHealth()
    {
        if (health <= 0)
        {
            entityDead = true;
            canMove = false;
            //Add death animation;
        }
    }

    public abstract void Move();
    private void GroundCheck()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheckTransform.position, circleRadius, groundMask);

        if (collider != null)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    public void Destroy()
    {
        if (health <= 0)
        {
            //Dying animation or just destroy it and instantiate particles etc..;
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {
            if (playerInRange)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawWireSphere(transform.position, attackRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckTransform.position, circleRadius);
        }
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
