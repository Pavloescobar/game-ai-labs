using UnityEngine;
using System.Collections.Generic;

public class RandomAgent : MonoBehaviour
{

    public GameObject waypointSet;  // Set from inspector

    // Waypoints
    protected Graph navGraph;
    protected List<GameObject> waypoints;
    protected Dictionary<int, GameObject> m_IdNodes_Map;
    protected int targetNode;

    public float speed;
    protected static float NEARBY = 0.2f;
    static System.Random rnd = new System.Random();

    void Start()
    {
        waypoints = new List<GameObject>();
        m_IdNodes_Map = new Dictionary<int, GameObject>();
        navGraph = new AdjacencyListGraph();
        findWaypoints();
        buildGraph();
    }

    void Update()
    {
        float dist = Vector3.Distance(targetPosition(), transform.position);
        if (dist < NEARBY)
        {
            List<int> nodes = navGraph.neighbours(targetNode);
            targetNode = nodes[rnd.Next(nodes.Count)];
            Debug.Log("Targeted waypoint " + targetNode);
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPosn = targetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPosn, speed);
    }

    Vector3 targetPosition()
    {
        return waypoints[targetNode].transform.position;
    }

    void findWaypoints()
    {
        if (waypointSet != null)
        {
            foreach (Transform t in waypointSet.transform)
            {
                waypoints.Add(t.gameObject);
                //get id of node
                WaypointNodeData nodeData = t.gameObject.GetComponent<WaypointNodeData>();
                if (nodeData != null)
                {
                    m_IdNodes_Map.Add(nodeData.NodeID, t.gameObject);
                }
            }
            Debug.Log("Found " + waypoints.Count + " waypoints.");
        }
        else
        {
            Debug.Log("No waypoints found.");
        }
    }

    void buildGraph()
    {
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
        // TO DO: add edges

        /*----remark: the following code constrcuts a specific map,
         *  not generic code, a more generic code can be constrcuted if required
         */
        //phase 1 -edges from node 0 to all other nodes
        for (int id = 1; id < 8; id++)
        {
            navGraph.addEdge(0, id);
        }

        //phase 2 -edges from each node to next node id (1-2, 2-3, 3-4,...) starting from id 1
        for (int id = 1; id < 7; id++)
        {
            navGraph.addEdge(id, id + 1);
        }

        //phase 3- closing edge from nodes 0 to 7
        navGraph.addEdge(7, 1);
        //for testing, log the neighbors of all nodes

        for (int id = 0; id < 8; id++)
        {
            List<int> neighborsList = navGraph.neighbours(id);
            string logMessage = "Node " + id + " is connected to: ";
            foreach (int n in neighborsList)
                logMessage += n + ",";

            Debug.Log(logMessage);
        }
    }
}