using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinateCoordinates;

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down };
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> grid;
    Queue<Node> frontier = new Queue<Node>();
    GridManager gridManager;

    Node currentSearchNode;
    Node startNode;
    Node destinateNode;

    public Vector2Int StartCoordinates { get => startCoordinates;}
    public Vector2Int DestinateCoordinates { get => destinateCoordinates;}

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null) {grid = gridManager.Grid;}
    }

    private void Start()
    {
        if (gridManager != null)
        {
            startNode = grid[startCoordinates];
            destinateNode = grid[destinateCoordinates];           
        }
        GetNewPath();
    }

    private void exploreNeighbors()
    {
        List<Node> neightbors = new List<Node>();
        
        foreach (var direction in directions)
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(neighborCoordinates))
            {
                neightbors.Add(grid[neighborCoordinates]);
            }
        }

        foreach (var neighbor in neightbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                reached.Add(neighbor.coordinates,neighbor);
                frontier.Enqueue(neighbor);
                neighbor.connetedTo = currentSearchNode;
            }
        }
    }

    private void BreathFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinateNode.isWalkable = true;

        frontier.Clear();
        frontier.Enqueue(grid[coordinates]);

        reached.Clear();
        reached.Add(coordinates, grid[coordinates]);

        bool isRunning = true;

        while (isRunning && frontier.Count > 0)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            exploreNeighbors();
            if (currentSearchNode.coordinates == destinateCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinateNode;
        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connetedTo != null)
        {
            currentNode = currentNode.connetedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();
        return path;
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);       
    }

    public List<Node> GetNewPath(Vector2Int currentCoordinates)
    {
        gridManager.ResetNodes();
        BreathFirstSearch(currentCoordinates);
        return BuildPath();
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }        
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath",false, SendMessageOptions.DontRequireReceiver);
    }

}
