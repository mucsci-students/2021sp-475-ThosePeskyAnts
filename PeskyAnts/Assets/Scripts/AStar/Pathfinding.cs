using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    
    PathRequestManager requestManager;
    public Grid grid; 

    void Awake ()
    {
        requestManager = GetComponent<PathRequestManager> ();
        grid = GetComponent<Grid>();
    }
    
    public void StartFindPath (Vector3 startPosition, Vector3 targetPosition)
    {
        StartCoroutine (FindPath (startPosition, targetPosition));
    }

    // Uses an A* algorithm to find the best path from startPosition
    // to targetPosition on the grid. 
    public IEnumerator FindPath (Vector3 startPosition, Vector3 targetPosition)
    {

        // make sure grid is up to date 
        // grid.CreateGrid ();

        Vector3[] waypoints = new Vector3[0]; 
        bool pathFound = false; 

        Node startNode = grid.NodeFromWorldPoint (startPosition);
        Node targetNode = grid.NodeFromWorldPoint (targetPosition);

        if (targetNode.walkable) 
        {

            // open set contains nodes to explore next
            // This is not the best data structure because 
            // we would need to search through (O(n)) to find the node 
            // with the lowest cost so we could change this to 
            // a priority queue or heap later for O(1)
            Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
            // closed set contains nodes that were already explored
            HashSet<Node> closedSet = new HashSet<Node> ();
            // First position to explore would be the start position
            openSet.Add (startNode);

            while (openSet.Count > 0)
            {
                // Find the node with the least fCost 
                Node currentNode = openSet.PopFront ();

                // Add node to the closedSet - we will have explored this node
                closedSet.Add (currentNode);

                // if current is the target node - path found 
                if (currentNode == targetNode)
                {
                    pathFound = true; 
                    // path found stop searching 
                    break; 
                }

                // Add all unexplored an traversable children nodes to the openSet
                List<Node> neighbors = grid.GetNeighbors (currentNode);
                foreach (Node neighbor in neighbors)
                {
                    // skip child if in the closedSet
                    // or skip child if not traversable - aka obstacles
                    if (closedSet.Contains (neighbor) || !neighbor.walkable)
                        continue; 

                    // if this path to child is shorter than another
                    // or if the child was not reached yet
                    int newMoveCost = currentNode.gCost + GetDistance (currentNode, neighbor);
                    if (newMoveCost < neighbor.gCost || !openSet.Contains (neighbor))
                    {
                        // set the fcost of the child 
                        neighbor.gCost = newMoveCost;
                        neighbor.hCost = GetDistance (neighbor, targetNode);
                        // set the parent of the child as the current node
                        neighbor.parent = currentNode; 
                        // add child to the openSet if it is not already there
                        if (!openSet.Contains (neighbor))
                            openSet.Add (neighbor);
                    }
                }
            }
        }
        // wait for 1 frame
        yield return null;

        if (pathFound)
        {
            waypoints = RetracePath (startNode, targetNode);
        }

        requestManager.FinishedProcessingPath (waypoints, pathFound);

    }

    Vector3[] RetracePath (Node startNode, Node endNode)
    {
        List<Node> path = new List<Node> ();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add (currentNode);
            currentNode = currentNode.parent; 
        }

        // remove unnecessary waypoints 
        Vector3[] waypoints = SimplifyPath (path);

        // change direction to go from start to end 
        Array.Reverse (waypoints);

        return waypoints; 

    }

    Vector3[] SimplifyPath (List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3> ();
        Vector2 directionOld = Vector2.zero;

        // add the end/target position 
        waypoints.Add (path[0].worldPosition);

        // add the rest of the waypoints 
        for (int i = 1; i < path.Count; ++i)
        {
            Vector2 directionNew = new Vector2 (path[i-1].gridX - path[i].gridX, path[i-1].gridY - path[i].gridY);
            // only add a waypoint if the direction changes 
            if (directionNew != directionOld)
                waypoints.Add (path[i].worldPosition);
            directionOld = directionNew; 
        }

        return waypoints.ToArray ();
    }

    int GetDistance (Node a, Node b)
    {
        int distX = Mathf.Abs (a.gridX - b.gridX);
        int distY = Mathf.Abs (a.gridY - b.gridY);

        if (distX > distY)
            return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }

}
