using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]

// Need to put this script in Editor Folder when build the game.

public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 1f); //orange color
 
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        DisplayLabel();
        label.enabled = false;
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayLabel();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColor();
        toggleLabels();
    }

    private void SetLabelColor()
    {
        if (gridManager == null){return;}
        Node node = gridManager.GetNode(coordinates);
        if(node == null) { return; }

        if (!node.isWalkable){ label.color = blockColor; }
        else if (node.isPath) { label.color = pathColor; }
        else if (node.isExplored) { label.color = exploredColor; }
        else { label.color = defaultColor; }
    }

    private void toggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    private void DisplayLabel()
    {
        if (gridManager == null) { return; }

        coordinates.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }


}
