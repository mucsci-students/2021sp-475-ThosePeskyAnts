using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : MonoBehaviour
{

    public bool hasAntReachedSugar = false; 

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.gameObject);
        // check if it was an ant 
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Ant"))
        {
            hasAntReachedSugar = true; 
        }
    }
}
