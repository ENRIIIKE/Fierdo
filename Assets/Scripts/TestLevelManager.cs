using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelManager : MonoBehaviour
{
    [Header("Weight Section")]
    public DistanceJoint2D distanceJoint;
    public HingeJoint2D hinge;

    [Header("Slime Spawn")]
    public Transform[] spawnPoint;
    public GameObject[] slime;
    public Transform entityParent;

    public List<GameObject> spawnedEntities = new List<GameObject>();
    public void ReleaseWeight()
    {
        distanceJoint.enabled = false;
        hinge.enabled = false;
    }
    public void SpawnSlime(int index)
    {
        GameObject spawnedSlime = Instantiate(slime[index], spawnPoint[index].position, Quaternion.identity, entityParent);
        spawnedEntities.Add(spawnedSlime);
    }
    public void KillAllSlimes()
    {
        foreach (GameObject entity in spawnedEntities)
        {
            Destroy(entity);
        }
        spawnedEntities.Clear();
    }
}
