using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private int index = 1;

    [Header("Tween")]
    public LeanTweenType type;
    public float smoothTime;


    [Header("Options")]
    [Tooltip("Reverse option will make platform move from starting waypoint to the last waypoint, and then from last waypoint back to the start waypoint")]
    public bool reverse;
    private bool goingReverse;

    [Space]
    [Tooltip("If you want instant moving platform point to point, then set the number to 0.")]
    public float waitTimer;
    void Start()
    {
        MovePlatform();
    }

    [ContextMenu("Reset Values")]
    private void ResetValues()
    {
        type = LeanTweenType.linear;
        smoothTime = 1f;

        reverse = false;
        waitTimer = 0f;

        Debug.Log("Values have been set to default");
    }

    private void MovePlatform()
    {
        //Debug.Log("Start Index: " + index);

        LeanTween.move(gameObject, waypoints[index].position, smoothTime).setEase(type).setOnComplete(MovePlatform).setDelay(waitTimer);

        if (reverse)
        {
            if (index < waypoints.Count - 1 && !goingReverse)
            {
                index++;
            }
            else
            {
                goingReverse = true;
            }

            if (goingReverse)
            {
                if (index == 0)
                {
                    goingReverse = false;
                    index++;
                }
                else
                {
                    index--;
                }
            }
        }
        else
        {
            index++;
        }


        if (index > waypoints.Count - 1 && !reverse)
        {
            index = 0;
        }
        //Debug.Log("After Index: " + index);
    }
}
