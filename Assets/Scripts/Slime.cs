using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBasic
{

    [Header("Slime Variables")]
    public float timeToAttackMin;
    public float timeToAttackMax;
    private float timeToAttack;

    public float movementSpeedMin;
    public float movementSpeedMax;


    public override void Move()
    {
        if (!canMove) return;

        canMove = false;

        movementSpeed = Random.Range(movementSpeedMin, movementSpeedMax);

        timeToAttack = Random.Range(timeToAttackMin, timeToAttackMax);

        if (transform.position.x > player.position.x)
        {
            rb.AddForce((Vector2.up + Vector2.left) * movementSpeed, ForceMode2D.Impulse);
        }
        else if (transform.position.x < player.position.x)
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
