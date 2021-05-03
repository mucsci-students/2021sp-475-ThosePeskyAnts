using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceBlock : MonoBehaviour
{

    public GameObject blockPrefab;
    public GameObject Level;
    public float blockLimit;
    public float blockActual;
    public bool rotateBlock = false;
    public Text numBlocksIndicator; 

    public float sightRange = 5f;

    public Grid grid; 

    void Update()
    {

        // Update number of blocks indicator
        numBlocksIndicator.text = (blockLimit - blockActual).ToString();

        // Right mouse button - rotates block 
        if (Input.GetMouseButtonDown(1))
        {
            if (rotateBlock == false) rotateBlock = true;
            else if (rotateBlock == true) rotateBlock = false;
            SoundManager.PlaySound ("Swoosh");
        }

        // Left mouse button 
        // - picks up a block
        // - places a block 
        if (Input.GetMouseButtonDown(0))
        {
            //SoundManager.PlaySound ("MetalTink");
            Vector3 mouse = Input.mousePosition;//Get the mouse Position
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);//Cast a ray to get where the mouse is pointing at
            RaycastHit hit;//Stores the position where the ray hit.
            //if raycast hit another prefab
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                // if a placed block is clicked remove it
                if(hit.transform.tag == "Block") 
                {
                    // move block out of level 
                    hit.collider.gameObject.transform.position = new Vector3 (0, -200, 0);
                    // get rid of object
                    Destroy(hit.collider.gameObject);
                    blockActual--;
                    // update ants' grid AI
                    grid.CreateGrid ();
                    SoundManager.PlaySound ("Bloop");

                }
                // else place a block
                else 
                {
                    // only place block if block limit not exceeded
                    if (blockActual != blockLimit) 
                    {
                        if (rotateBlock == false) Instantiate(blockPrefab, hit.point, Quaternion.identity);//create a cube at the location of the mouse click
                        else if (rotateBlock == true) Instantiate(blockPrefab, hit.point, Quaternion.Euler(0, 90, 0));//create a cube at the location of the mouse click rotated 90 degrees
                        blockActual++;
                        SoundManager.PlaySound ("MetalTink");
                    }
                    // update ants' grid AI
                    grid.CreateGrid ();
                }
            }

        }

    }

    public bool hasBlocks ()
    {
        return blockActual != blockLimit;
    }
}
