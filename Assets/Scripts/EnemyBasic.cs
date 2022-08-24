using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBasic : MonoBehaviour
{
    [Header("General")]
    public Transform player;
    public Rigidbody2D rb;
    public bool showGizmos = false;


    [Header("Stats")]
    public int health;
    public int damage;
    public float attackSpeed;
    public bool canAttack = true;
    public float movementSpeed;
    public float knockbackStrength;

    [Header("Range")]
    public float attackRange;
    public bool playerInRange;

    public void Update()
    {
        float distance = Vector2.Distance(this.transform.position, player.position);
        if (distance < attackRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    public void Attack()
    {
        if (!canAttack) return;
        canAttack = false;

        Debug.Log("Player Damaged");
        //Attack player

        PlayerController.Instance.Knockback(transform, knockbackStrength);

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
    }

    public abstract void Move();

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
        }
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
