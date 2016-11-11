using UnityEngine;
using System.Collections.Generic;

public class WaypointGraph
{

    public Graph navGraph;
    protected List<GameObject> waypoints;
    public GameObject this[int i]
    {
        get { return waypoints[i]; }
        set { waypoints[i] = value; }
    }

    public WaypointGraph(GameObject waypointSet)
    {

        waypoints = new List<GameObject>();
        navGraph = new AdjacencyListGraph();

        findWaypoints(waypointSet);
        buildGraph();
    }

    private void findWaypoints(GameObject waypointSet)
    {

        if (waypointSet != null)
        {
            foreach (Transform t in waypointSet.transform)
            {
                waypoints.Add(t.gameObject);
            }
            Debug.Log("Found " + waypoints.Count + " waypoints.");

        }
        else
        {
            Debug.Log("No waypoints found.");

        }
    }

    private void buildGraph()
    {

        int n = waypoints.Count;

        navGraph = new AdjacencyListGraph();
        for (int i = 0; i < waypoints.Count; i++)
        {
            //navGraph.addNode(i);

            //get ID of each node
            GameObject currentNode = waypoints[i];
            WaypointNodeData nodeData = currentNode.gameObject.GetComponent<WaypointNodeData>();
            if (nodeData != null)
            {
                navGraph.addNode(nodeData.NodeID);
            }

        }

        // ADD APPROPRIATE EDGES

        /*----remark: the following code constrcuts a specific map,
        *  not generic code, a more generic code can be constrcuted if required
        */
        //phase 1 -edges from node 0 to all other nodes
        double cost;
        for (int id = 1; id < 8; id++)
        {
            cost = CalculateCost(0, id);
            navGraph.addEdge(0, id, cost);
        }

        //phase 2 -edges from each node to next node id (1-2, 2-3, 3-4,...) starting from id 1
        for (int id = 1; id < 7; id++)
        {
            cost = CalculateCost(id, id+1);
            navGraph.addEdge(id, id + 1, cost);
        }

        //phase 3- closing edge from nodes 0 to 7
        cost = CalculateCost(7, 1);
        navGraph.addEdge(7, 1, cost);
       
        
        //for testing, log the neighbors of all nodes
        for (int id = 0; id < 8; id++)
        {
            List<int> neighborsList = navGraph.neighbours(id);
            string logMessage = "Node " + id + " is connected to: ";
            foreach (int g in neighborsList)
            {
                double? currentCost = navGraph.getCost(id, g);
                if(currentCost!=null)
                    logMessage += g  +"(Cost= "+ currentCost.Value+"),";
                else
                    logMessage += g + "(Cost= null),";
            }

            Debug.Log(logMessage);
        }

    }

    private double CalculateCost(int idA, int idB)
    {
        Vector3 startPoint = this[idA].transform.position;
        Vector3 endPoint = this[idB].transform.position;
        double cost = Vector3.Distance(startPoint, endPoint);
        return cost;
    }


    public int? findNearest(Vector3 here)
    {
        int? nearest = null;

        if (waypoints.Count > 0)
        {
            nearest = 0;
            Vector3 there = waypoints[0].transform.position;
            float minDistance = Vector3.Distance(here, there);

            for (int i = 1; i < waypoints.Count; i++)
            {
                there = waypoints[i].transform.position;
                float distance = Vector3.Distance(here, there);
                if (distance < minDistance)
                {
                    nearest = i;
                }
            }
        }
        return nearest;
    }

   
}

