using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntAI : MonoBehaviour
{

    public float movementSpeed = 1.0f; 
    public float repathSpeed = 2.0f; 
    float lastRepath = 0.0f; 
    public Rigidbody rigidbody; 
    public Transform sugar; 

    public Pathfinding pathfinding; 
    public Vector3[] path; 
    public int targetIndex; 

    // Start is called before the first frame update
    void Start()
    {
        sugar = GameObject.Find("EndGoal").transform; 
        pathfinding = GameObject.Find ("A*").GetComponent<Pathfinding> ();
        PathRequestManager.RequestPath (transform.position, sugar.position, OnPathFound, gameObject);
    }

    public void OnPathFound (Vector3[] newPath, bool pathFound)
    {
        if (pathFound)
        {
            path = newPath;
            targetIndex = 0; 
            StopCoroutine ("FollowPath");
            StartCoroutine ("FollowPath");   
        }
    }

    void FixedUpdate()
    {
        // only update path based on speed 
        if (Time.time > lastRepath + repathSpeed)
        {
            // request a new path
            PathRequestManager.RequestPath (transform.position, sugar.position, OnPathFound, gameObject);
            lastRepath = Time.time;
        }

    }

    IEnumerator FollowPath ()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (Vector3.Distance(transform.position, currentWaypoint) < 0.1)
            {
                ++targetIndex; 
                if (targetIndex >= path.Length)
                    yield break;
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, movementSpeed * Time.deltaTime);
            yield return null; 
        }

    }

    public void OnDrawGizmos ()
    {
        if (path != null)
            for (int i = targetIndex; i < path.Length; ++i)
            {
                Gizmos.color = Color.cyan;
                if (i == targetIndex)
                {
                    Gizmos.DrawLine (transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine (path[i-1], path[i]);
                }
                Gizmos.DrawSphere (path[i], 0.1f);
            }
    }
}
