using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class DijkstraPathFinder: Pathfinder
    {
        public override List<int> findPath(int a, int b)
        {
            /*based on :
            1- https://github.com/mburst/dijkstras-algorithm/blob/master/dijkstras.cs
            2- https://code.msdn.microsoft.com/windowsdesktop/Dijkstras-Single-Soruce-69faddb3
            3- http://www.codeproject.com/Articles/24816/A-Fast-Priority-Queue-Implementation-of-the-Dijkst
            */

            //Dictionary < int,int?> previousHub = new Dictionary<int, int?>();
            Dictionary<int, float> distances = new Dictionary<int, float>();
            List<int> allNodes = navGraph.nodes();
            //List<int> nodeIndices = new List<int>();

            List<int> path = new List<int>();
            SimplePriorityQueue<int> priorityQueue = new SimplePriorityQueue<int>();

            foreach (var nodeIndex in allNodes)
            {
                if (nodeIndex == a)
                {
                    distances[nodeIndex] = 0;
                }
                else
                {
                    distances[nodeIndex] = float.MaxValue;//undefined
                }

                //nodeIndices.Add(nodeIndex);
                //previousHub.Add(nodeIndex, null);//initial
                priorityQueue.Enqueue(nodeIndex, distances[nodeIndex]);
            }
            while (priorityQueue.Count > 0)
            {
                int smallestNode = priorityQueue.Dequeue();
                List<int> neighborsOfSmallest = navGraph.neighbours(smallestNode);
                path.Add(smallestNode);
                foreach (int v in neighborsOfSmallest)
                {
                    
                    double? edgeCost = navGraph.getCost(smallestNode, v);
                    if (edgeCost != null)
                    {
                        float currentDistance = distances[v];
                        float altDistance = distances[smallestNode] + (float)edgeCost.Value;
                        if (altDistance < currentDistance)
                        {
                            distances[v] = altDistance;
                            //previousHub[v] = smallestNode;
                            priorityQueue.UpdatePriority(v, altDistance);
                        }
                    }
                }
            }
           
            return path;
        }
       
    }
}
