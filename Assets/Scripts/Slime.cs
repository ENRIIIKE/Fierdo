using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBasic
{


    public override void Attack()
    {
        rb.AddForce((Vector2.up + Vector2.right) * speed);
    }
}
