using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrigger : MonoBehaviour
{
    private Slime slimeScript;
    private void Start()
    {
        GameObject parent = transform.parent.gameObject;
        slimeScript = parent.GetComponent<Slime>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            slimeScript.Attack();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            slimeScript.Attack();
        }
    }
}
