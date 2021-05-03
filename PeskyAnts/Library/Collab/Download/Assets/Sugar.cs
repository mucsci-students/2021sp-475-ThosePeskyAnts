using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : MonoBehaviour
{

    public bool hasAntReachedSugar = false; 

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        // check if it was an ant 
        if (other.gameObject.layer == LayerMask.NameToLayer("Ant"))
        {
            hasAntReachedSugar = true; 
        }
    }
}
