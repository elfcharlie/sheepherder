using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform oldMan;
    public Transform dog;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;
    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;
    public List<Node> path;

    void Awake()
    {

        nodeDiameter = 2 * nodeRadius;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new Node[gridSizeX,gridSizeY];
        Vector2 worldBottomLeft = new Vector2(transform.position.x - gridWorldSize.x / 2, transform.position.y - gridWorldSize.y / 2);

        for (int x=0; x < gridSizeX; x++)
        {
            for (int y=0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius)
                    + Vector2.up * (y * nodeDiameter +  nodeRadius);
                    bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                    grid[x,y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x  = Mathf.RoundToInt((gridSizeX) * percentX);
        int y  = Mathf.RoundToInt((gridSizeY) * percentY);
        return grid[x,y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX <= gridSizeX && checkY >= 0 && checkY <= gridSizeY)
                {
                    neighbours.Add(grid[checkX,checkY]);
                }
            }  
        }
        return neighbours;
    }

    void OnDrawGizmos()
    {
        if (grid != null)
        {
            Node oldManNode = NodeFromWorldPoint(oldMan.position);
            Node dogNode = NodeFromWorldPoint(dog.position);
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable)?Color.white:Color.red;
                if(path!=null){
                    if(path.Contains(n))
                    {
                        Gizmos.color = Color.blue;
                    }
                }
                Gizmos.DrawCube(new Vector3(n.worldPosition.x, n.worldPosition.y, 0), Vector3.one * (nodeDiameter - 0.5f));
                
                if(oldManNode == n)
                {
                    Gizmos.color = Color.cyan;
                }
                if (dogNode == n)
                {
                    Gizmos.color = Color.green;
                }
                if (path[0] == n)
                {
                    Gizmos.color = Color.magenta;
                }
                Gizmos.DrawCube(new Vector3(n.worldPosition.x, n.worldPosition.y, 0), Vector3.one * (nodeDiameter - 0.5f));

            }
        }
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
    }
}
