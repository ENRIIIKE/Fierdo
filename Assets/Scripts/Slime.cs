using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBasic
{

    [Header("Slime Variables")]
    public float timeToAttackMin;
    public float timeToAttackMax;
    private float timeToAttack;

    public bool canMove = true;


    private new void Update()
    {
        Move();
    }
    public override void Move()
    {
        if (!canMove) return;

        canMove = false;

        timeToAttack = Random.Range(timeToAttackMin, timeToAttackMax);

        if (this.transform.position.x > player.position.x)
        {
            rb.AddForce((Vector2.up + Vector2.left) * movementSpeed, ForceMode2D.Impulse);
        }
        else if (this.transform.position.x < player.position.x)
        {
            rb.AddForce((Vector2.up + Vector2.right) * movementSpeed, ForceMode2D.Impulse);
        }

        StartCoroutine(StartDelay());
    }
    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(timeToAttack);
        canMove = true;
    }
}
