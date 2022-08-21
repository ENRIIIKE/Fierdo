using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBasic : MonoBehaviour
{
    [Header("General")]
    public int id;
    public new string name;
    public string description;

    public Rigidbody2D rb;

    [Header("Stats")]
    public int health;
    public int damage;
    public int attackSpeed;
    public float speed;

    public void SetVariables()
    {

    }

    public abstract void Attack();

    public void Destroy()
    {

    }
}
