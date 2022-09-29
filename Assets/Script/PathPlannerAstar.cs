using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlannerAstar: MonoBehaviour
{

    Grid grid;
    public Transform oldMan;
    public Transform dog;
    public Transform dogStartPos;
    public Transform oldManStartPos;
    private OldManController oldManController;
    void Awake()
    {
        grid = GetComponent<Grid>();
        oldManController = GameObject.FindWithTag("OldMan").GetComponent<OldManController>();
        oldManController.SetNode(grid.NodeFromWorldPoint(oldMan.position));

    }

    void Update()
    {
        astar(oldMan.position, dog.position);
        oldManController.SetNode(grid.NodeFromWorldPoint(oldMan.position));
    }
    public void astar(Vector2 startPos, Vector2 goalPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node goalNode = grid.NodeFromWorldPoint(goalPos);
        if (!goalNode.walkable)
        {
            goalNode = grid.NodeFromWorldPoint(dogStartPos.position);
        }
        
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost 
                    || openSet[i].fCost == node.fCost)
                {
                    if(openSet[i].heuristicCost < node.heuristicCost)
                    {
                        node = openSet[i];
                    }
                }
            }
            openSet.Remove(node);
            closedSet.Add(node);

            if (node == goalNode)
            {
                RetracePath(startNode, goalNode);
                return;
            }
            
            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = node.costToGo + GetDistance(node, neighbour);
                if (newMovementCostToNeighbour < neighbour.costToGo || !openSet.Contains(neighbour))
                {
                    neighbour.costToGo = newMovementCostToNeighbour;
                    neighbour.heuristicCost = GetDistance(neighbour, goalNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    void RetracePath(Node startNode, Node goalNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = goalNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        grid.path = path;
        oldManController.SetPath(path);

    }

    public int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }

}
