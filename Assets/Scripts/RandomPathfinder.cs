using System.Collections.Generic;
using UnityEngine;

public class RandomPathfinder : Pathfinder {
    public override List<int> findPath(int start, int goal) {
        // Implement this
        //using index as same as ID in this implementation
        int current = start;
        List<int> path = new List<int>();
        Random rand = new Random();
        path.Add(current);
        while (current != goal)
        {
           
            List<int> currentNeighbors= this.navGraph.neighbours(current);
            int next = Random.Range(0, currentNeighbors.Count);
            int nextNodeIndex = currentNeighbors[next];
            path.Add(nextNodeIndex);

            current = nextNodeIndex;
        }
        
        
        return path;
    }
}