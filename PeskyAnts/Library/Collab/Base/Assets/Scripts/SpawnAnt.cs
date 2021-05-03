using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnt : MonoBehaviour
{

    public GameObject endGoal;
    public GameObject antParent;
    public GameObject antPrefab;
    public GameObject antSpawn;
    public float antLimit;
    private float antCount;
    public float antSpawnRate = 1f; //change the speed that food spawns
    private float lastSpawned; //used for creating the delay in spawning the food

    void Update()
    {
        if (Time.time > lastSpawned + antSpawnRate)
        {
            if (antCount != antLimit)
            {
                GameObject temp = Instantiate(antPrefab, antSpawn.transform.position, Quaternion.identity, antParent.transform);//create an ant at the location of antSpawn
                antCount++;
                temp.GetComponent<MoveAnt>().nmAgent.SetDestination(endGoal.transform.position);//reset the destination of the ant to the endGoal
            }
            lastSpawned = Time.time;
        }
    }
}
