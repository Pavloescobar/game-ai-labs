using UnityEngine;
using System.Collections.Generic;


class AStarAgent:PathAgent
{
    public override Pathfinder createPathfinder()
    {
        Heuristic costFunction = EuclideanDistance;
        return new AStarPathfinder(costFunction);
    }

    /*Heuristic function that return the  euclidean distance
    *  between a node and a goal in graph*/
    public float EuclideanDistance(int next, int goal)
    {
        Vector3 nextPoint = this.waypoints[next].transform.position;
        Vector3 goalPoint = this.waypoints[goal].transform.position;

        return Vector3.Distance(nextPoint, goalPoint);
    }
}

