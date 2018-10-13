using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NodeRecordPool
{
    public static Queue<PathFinding.NodeRecord> pool = new Queue<PathFinding.NodeRecord>();

    public static void InstantiatePool(int max)
    {
        int actualLength = pool.Count;
        int diference = max - actualLength;
        if (diference > 0)
        {
            Mathf.Abs(diference);
            for (int i = 0; i < diference; i++)
            {
                PathFinding.NodeRecord newNodeRecord = new PathFinding.NodeRecord();
                pool.Enqueue(newNodeRecord);
            }
        }
    }
    public static void InstantiatePool(int width, int heigth)
    {
        int max = width * heigth;

        int actualLength = pool.Count;
        int diference = max - actualLength;
        if (diference > 0)
        {
            Mathf.Abs(diference);
            for (int i = 0; i < diference; i++)
            {
                PathFinding.NodeRecord newNodeRecord = new PathFinding.NodeRecord();
                pool.Enqueue(newNodeRecord);
            }
        }
    }
    
}
