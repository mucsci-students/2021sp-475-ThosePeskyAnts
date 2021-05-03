using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool walkable; 
    public Vector3 worldPosition; 
    public int gridX;
    public int gridY;

    public Node parent; 
    public int gCost; 
    public int hCost; 
    int heapIndex; 

    public Node (bool _walkable, Vector3 _worldPosition, int x, int y)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = x;
        gridY = y; 
    }

    public int fCost 
    {
        get 
        {
            return gCost + hCost; 
        }
    }

    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public int CompareTo (Node other)
    {
        int compare = fCost.CompareTo (other.fCost);
        if (compare == 0)
            compare = hCost.CompareTo (other.hCost);
        return -compare; 
    }
}
