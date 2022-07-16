using System.Collections.Generic;
using System;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    public event Action<Figure> OnActionFigureWasPlaced;
    [SerializeField] private GameObject prefabCell;
    private int numCell = 64;
    private int horizontal_lines = 8;
    private List<Cell> cellsList = new List<Cell>();



    public void Generate()
    {
        for(int i = 1; i <= numCell; i++)
        {
            //get name of the cell
            var line_horizontal = Mathf.FloorToInt(i/8.1f) + 1;
            var live_vertical = Constants.vertical[i-1 -((line_horizontal - 1)*horizontal_lines)];

            var cell = Instantiate(prefabCell, Vector3.zero, Quaternion.identity, this.transform);
            cell.gameObject.name = $"{live_vertical}{line_horizontal}";
            cellsList.Add(cell.GetComponent<Cell>()); 
            cell.GetComponent<Cell>().OnActionFigureWasPlaced += OnActionFigureWasPlacedHandler;
        }
    } 

    void OnDisable()
    {
        foreach(Cell e in cellsList)
        {
            e.OnActionFigureWasPlaced -= OnActionFigureWasPlacedHandler;
        }
    }

    void OnEnable()
    {
        foreach(Cell e in cellsList)
        {
            e.OnActionFigureWasPlaced += OnActionFigureWasPlacedHandler;
        }
    }

    private void OnActionFigureWasPlacedHandler(Figure figure)
    {
        OnActionFigureWasPlaced?.Invoke(figure);
    }
}
