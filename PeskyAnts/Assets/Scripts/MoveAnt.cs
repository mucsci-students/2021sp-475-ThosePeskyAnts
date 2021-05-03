using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnt : MonoBehaviour
{

    public UnityEngine.AI.NavMeshAgent nmAgent;
    public GameObject endGoal;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//If the player has left clicked
        {
            nmAgent.SetDestination(endGoal.transform.position);//reset the destination of the ant to the endGoal
        }
    }
}