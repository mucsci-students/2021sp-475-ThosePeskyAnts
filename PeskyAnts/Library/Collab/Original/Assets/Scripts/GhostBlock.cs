using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlock : MonoBehaviour
{

    public PlaceBlock placeBlockScript; 
    float clickCount = 0;
    public bool IsTouchingSugar = false;

    void Update()
    {
        Vector3 mouse = Input.mousePosition;//Get the mouse Position
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);//Cast a ray to get where the mouse is pointing at
        RaycastHit hit;//Stores the position where the ray hit.
                       //if raycast hit another prefab

        // rotate block 
        if (placeBlockScript.rotateBlock)
            transform.rotation = Quaternion.Euler (0, 90, 0);
        else
            transform.rotation = Quaternion.Euler (0, 0, 0);

        //If the raycast hits something
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            // move the ghost block to that position 
            transform.position = hit.point;
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

        void OnTriggerEnter (Collider other) {
            //if the ghost block is touching the sugar update bool
            if (other.gameObject.CompareTag("Sugar")) IsTouchingSugar = true;
            else IsTouchingSugar = false;
        }

        // if (Input.GetMouseButtonDown(1))
        // {
        //     if (clickCount%2 == 0) transform.rotation = Quaternion.Euler(0, 90, 0);
        //     else transform.rotation = Quaternion.Euler(0, 0, 0);
        //     clickCount++;
        // }
    }
}
