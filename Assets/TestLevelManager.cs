using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelManager : MonoBehaviour
{
    [Header("Weight Section")]
    public DistanceJoint2D distanceJoint;
    public HingeJoint2D hinge;
    public void ReleaseWeight()
    {
        distanceJoint.enabled = false;
        hinge.enabled = false;
    }
}
