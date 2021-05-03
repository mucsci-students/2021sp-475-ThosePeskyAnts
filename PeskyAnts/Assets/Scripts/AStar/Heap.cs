using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount; 

    public Heap (int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    public void Add (T item)
    {
        item.HeapIndex = currentItemCount; 
        items[currentItemCount] = item; 
        UpHeap (item);
        ++currentItemCount;
    }

    void UpHeap (T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
                Swap (item, parentItem);
            else
                break; 
            
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    public void UpdateItem (T item)
    {
        UpHeap (item);
    }

    public int Count 
    {
        get {
            return currentItemCount;
        }
    }

    public bool Contains (T item)
    {
        return Equals (items[item.HeapIndex], item);
    }

    public T PopFront ()
    {
        T firstItem = items[0];
        --currentItemCount;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        DownHeap (items[0]);
        return firstItem;
    }

    void DownHeap (T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0; 

            // left child is larger than parent
            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;
                // right child is larger than parent 
                if (childIndexRight < currentItemCount)
                    // right child is larger than left child
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                        // parent should swap with the right child 
                        swapIndex = childIndexRight;

                // child is larger than parent 
                if (item.CompareTo(items[swapIndex]) < 0 )
                    Swap (item, items[swapIndex]);
                // parent item is larger than children 
                else
                    // done down heaping
                    return;

            }
            // parent does not have any children
            else
                // done down heaping
                return;


        }
    }

    void Swap (T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int temp = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = temp; 
    }

}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex {
        get;
        set; 
    }
}