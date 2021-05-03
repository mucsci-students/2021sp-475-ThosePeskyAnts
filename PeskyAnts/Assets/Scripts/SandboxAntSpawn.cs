using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxAntSpawn : MonoBehaviour
{
    public GameObject endGoal;
    public GameObject antParent;
    public GameObject antPrefab;
    public GameObject antSpawn;
    public GameObject antSpawn2;
    public float antLimit;
    private float antCount;
    public float antSpawnRate = 1f; //change the speed that food spawns
    private float lastSpawned; //used for creating the delay in spawning the food

    void Update()
    {
        if (Time.time > lastSpawned + antSpawnRate)
        {
            if (antParent.transform.childCount <= antLimit)
            {
                    if (antCount % 2 == 1) Instantiate(antPrefab, antSpawn.transform.position, Quaternion.identity, antParent.transform);
                    else Instantiate(antPrefab, antSpawn2.transform.position, Quaternion.identity, antParent.transform);
                    antCount++;
            }
            lastSpawned = Time.time;
        }
    }
}
