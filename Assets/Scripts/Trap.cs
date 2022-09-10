using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int trapDamage;
    public int knockbackStrength;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamagable>() != null)
        {
            IDamagable doDamage = collision.GetComponent<IDamagable>();

            doDamage.GetDamage(trapDamage, knockbackStrength, transform);
        }
    }
}
