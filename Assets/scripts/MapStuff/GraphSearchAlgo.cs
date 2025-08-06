using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GraphSearchAlgo 
{
    public static Dictionary<Vector2Int, Vector2Int?> BFS(Grid mapGraph, Vector2Int start, int movement)
    {
        Dictionary<Vector2Int, Vector2Int?> vistedNodes = new Dictionary<Vector2Int, Vector2Int?>();
        Dictionary<Vector2Int, int> costSoFar = new Dictionary<Vector2Int, int>();
        Queue<Vector2Int> nodesToVisitQueue = new Queue<Vector2Int>();
        
        nodesToVisitQueue.Enqueue(start);
        costSoFar.Add(start, 0);
        vistedNodes.Add(start, null);
        while ( nodesToVisitQueue.Count > 0)
        {
            Vector2Int currentNode = nodesToVisitQueue.Dequeue();
            foreach (Vector2Int neighbourPosition in mapGraph.GetNeighboursFor(currentNode) )
            {
                if(mapGraph.CheckIfPositionIsValid(neighbourPosition)== false)
                    continue;

                int nodeCost = mapGraph.GetMovementCost(neighbourPosition);
                int currentcost = costSoFar[currentNode];
                int newCost = currentcost + nodeCost;

                if (newCost <= movement)
                {
                    if (!vistedNodes.ContainsKey(neighbourPosition))
                    {
                        vistedNodes[neighbourPosition] = currentNode;
                        costSoFar[neighbourPosition] = newCost;
                        nodesToVisitQueue.Enqueue(neighbourPosition);
                    }
                    else if (costSoFar[neighbourPosition]> newCost)
                    {
                        costSoFar[neighbourPosition] = newCost;
                        vistedNodes[neighbourPosition] = currentNode;

                    }
                }
            }
            
        }

        return vistedNodes;
    }
    
  
}
