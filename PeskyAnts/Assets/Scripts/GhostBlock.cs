using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlock : MonoBehaviour
{

    public PlaceBlock placeBlockScript; 
    float clickCount = 0;
    public Material greenColor;
    public Material defaultColor;
    public GameObject blockParent;
    private GameObject blockSub;

    void Update()
    {
        // If paused 
        if (Time.timeScale == 0)
        {
            // hide block 
            transform.position = new Vector3(0, -100, 0);
            return; 
        }

        // Vector3 mouse = Input.mousePosition;//Get the mouse Position
        // Ray castPoint = Camera.main.ScreenPointToRay(mouse);//Cast a ray to get where the mouse is pointing at
        Transform cam = Camera.main.transform; 
        RaycastHit hit;//Stores the position where the ray hit.
                       //if raycast hit another prefab

        // rotate block 
        if (placeBlockScript.rotateBlock)
            transform.rotation = Quaternion.Euler (0, 90, 0);
        else
            transform.rotation = Quaternion.Euler (0, 0, 0);

        //If the raycast hits something
        if (Physics.Raycast(cam.position, cam.forward, out hit, Mathf.Infinity))
        {
            // move the ghost block to that position 
            transform.position = hit.point;
            //if sees placed block 
            if (hit.transform.tag == "Block")
            {
                // change placed blocks color to green
                hit.collider.gameObject.GetComponent<MeshRenderer>().material = greenColor;
                // hide block 
                transform.position = new Vector3(0, -100, 0);
                // saves the last looked at placed block for future reference (ie the next line)
                blockSub = hit.collider.gameObject;
                // check to change block's color to default
                // this ensures multiple blocks wont turn green
                foreach (Transform child in blockParent.transform)
                {
                    if (child.gameObject != blockSub) child.gameObject.GetComponent<MeshRenderer>().material = defaultColor;
                }
            }
            //single exception for the fake block in level 14
            else if (hit.transform.tag == "Fake")
            {
                foreach (Transform child in hit.transform.transform)
                {
                    if (child.gameObject != blockSub) child.gameObject.GetComponent<MeshRenderer>().material = greenColor;
                }
            }
            else
            {
                // change all placed block's color's to default
                foreach (Transform child in blockParent.transform)
                {
                    //single exception for fake block level 14
                    if (child.transform.tag == "Fake")
                    {
                        foreach (Transform child2 in child.transform)
                        {
                            child2.gameObject.GetComponent<MeshRenderer>().material = defaultColor;
                        }
                    }
                    //changes color of block back to default when no longer looking at it
                    else child.gameObject.GetComponent<MeshRenderer>().material = defaultColor;
                }
            }
        }
        // raycast doesnt hit anything 
        else
        {
            // hide block 
            transform.position = new Vector3(0, -100, 0);
            
        }


        // if the player does not have any blocks
        if (!placeBlockScript.hasBlocks())
        {
            // hide block
            transform.position = new Vector3(0, -100, 0);
        }
       

        // if (Input.GetMouseButtonDown(1))
        // {
        //     if (clickCount%2 == 0) transform.rotation = Quaternion.Euler(0, 90, 0);
        //     else transform.rotation = Quaternion.Euler(0, 0, 0);
        //     clickCount++;
        // }
    }
}
