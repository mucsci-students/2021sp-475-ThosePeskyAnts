﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest> ();
    PathRequest currentPathRequest; 

    static PathRequestManager instance; 
    Pathfinding pathfinding;

    bool isProcessingPath; 

    void Awake ()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding> ();
    }

    struct PathRequest {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;
        public GameObject requestee; 

        public PathRequest (Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback, GameObject r)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
            requestee =r;
        }
    }

    public static void RequestPath (Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback, GameObject r)
    {
        PathRequest newRequest = new PathRequest (pathStart, pathEnd, callback, r);
        instance.pathRequestQueue.Enqueue (newRequest);
        instance.TryProcessNext ();
    
    }

    void TryProcessNext ()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue ();
            isProcessingPath = true;
            // update grid 
            pathfinding.grid.CreateGrid ();
            // find path 
            pathfinding.StartFindPath (currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath (Vector3[] path, bool success)
    {
        if (currentPathRequest.requestee != null)
            currentPathRequest.callback (path, success);
        isProcessingPath = false;
        TryProcessNext ();
    }


}
