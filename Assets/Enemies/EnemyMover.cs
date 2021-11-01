using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField] [Range(0,10)]float enemiesSpeed = 10f;
    GridManager gridManager;
    PathFinder pathFinder;

    Enemy enemy;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {       
        ReturnToStart();       
        RecalculatePath(true);
    }  

    private void RecalculatePath(bool resetPath)
    {       
        Vector2Int coordinate = new Vector2Int();
        if (resetPath)
        {
            coordinate = pathFinder.StartCoordinates;
        }
        else
        {
            coordinate = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinate);
        StartCoroutine(EnemiesFollowPath());
    }

    private IEnumerator EnemiesFollowPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * enemiesSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        ReachTheEndPath();
    }

    private void ReachTheEndPath()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

}
