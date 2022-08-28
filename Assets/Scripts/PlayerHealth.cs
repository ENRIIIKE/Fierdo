using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;

    [Header("Health")]
    public int health;
    public int maxHealth;

    public bool playerDead;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void GetDamage(int damage)
    {
        if (damage > health)
        {
            health = 0;
        }
        else
        {
            health -= damage;
        }

        //Make death particles or something like that;
        CheckHealth();
    }
    public void CheckHealth()
    {
        if (health <= 0)
        {
            playerDead = true;
            playerController.canMove = false;
            playerController.animator.SetBool("isDead", true);
        }
    }
}
