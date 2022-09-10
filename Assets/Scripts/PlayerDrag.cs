using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrag : MonoBehaviour
{
    private PlayerController controller;

    public bool dragging;

    public Transform checkTransform;
    public float checkRadius;
    public LayerMask layerMask;

    public Transform foundObject;

    public float draggingSpeed;
    private float oldSpeed;
    private void Start()
    {
        controller = GetComponent<PlayerController>();

        oldSpeed = controller.movementSpeed;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DragObject();
        }
    }
    private void DragObject()
    {
        Collider2D collider = Physics2D.OverlapCircle(checkTransform.position, checkRadius, layerMask);

        if (dragging)
        {
            foundObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            foundObject.GetComponent<Collider2D>().enabled = true;
            foundObject.SetParent(null);

            dragging = false;
            foundObject = null;

            controller.animator.SetBool("isDragging", false);
            controller.movementSpeed = oldSpeed;
            controller.canRotate = true;
            controller.canJump = true;
            return;
        }

        if (collider != null && !dragging && controller.grounded)
        {
            foundObject = collider.transform;

            DraggableObject objectDragScript = collider.GetComponent<DraggableObject>();

            foundObject.SetParent(transform);
            foundObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            foundObject.GetComponent<Collider2D>().enabled = false;

            float offset = objectDragScript.offsetX;

            if (foundObject.transform.position.x > transform.position.x)
            {
                foundObject.position = new Vector2(transform.position.x + offset, transform.position.y);
            }
            else
            {
                foundObject.position = new Vector2(transform.position.x + offset, transform.position.y);
            }

            controller.canRotate = false;
            controller.canJump = false;
            controller.movementSpeed = draggingSpeed;
            controller.animator.SetBool("isDragging", true);

            dragging = true;
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkTransform.position, checkRadius);
    }
}
