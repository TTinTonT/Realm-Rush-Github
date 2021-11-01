using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize = 10;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    public Dictionary<Vector2Int, Node> Grid { get => grid;}
    public int UnityGridSize { get => unityGridSize;}

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates)) {return grid[coordinates];}
        return null;
    }

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                Node node = new Node(coordinates, true);

                grid.Add(coordinates, node);
            }
        }
    }

    public void BlockedNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(position.x / UnityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / UnityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();

        position.x = coordinates.x * UnityGridSize;
        position.z = coordinates.y * UnityGridSize;

        return position;
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connetedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

}
