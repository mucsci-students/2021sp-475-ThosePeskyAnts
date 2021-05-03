using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : MonoBehaviour
{

    public bool hasAntReachedSugar = false;
    public bool isTouchingGhost = false;
    public GameObject ghostBlock;
    public Material ghostBlockRed;
    public Material ghostBlockDefault;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        // check if it was an ant 
        if (other.gameObject.layer == LayerMask.NameToLayer("Ant"))
        {
            hasAntReachedSugar = true; 
        }
        // used to prevent player from placing blocks on sugar
        if (other.tag == "Ghost")
        {
            isTouchingGhost = true;
            //changes ghost blocks color to red
            ghostBlock.GetComponent<MeshRenderer>().material = ghostBlockRed;
        }
    }

    //all used in determining if the ghost block is touching the sugar or not
    void OnTriggerExit(Collider other)
    {
        //updates isTouchingGhost if no longer touching sugar
        if (other.tag == "Ghost")
        {
            isTouchingGhost = false;
            //changes ghost blocks color to normal
            ghostBlock.GetComponent<MeshRenderer>().material = ghostBlockDefault;
        }
    }
    public bool isTouchingGhostCheck ()
    {
        return isTouchingGhost;
    }
}
