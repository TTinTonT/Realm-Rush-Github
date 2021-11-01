using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isSuccessful = true;
    public bool IsSccessful { get => isSuccessful;}

    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();
    PathFinder pathFinder;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!IsSccessful)
            {
                gridManager.BlockedNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates) && towerPrefab.CreateTower(towerPrefab, transform.position))
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            
            gridManager.BlockedNode(coordinates);
            pathFinder.NotifyReceivers();
        }
    }
}
