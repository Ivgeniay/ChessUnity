using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    //prefab of a cell
    [SerializeField] private GameObject prefabCell;
    private int numCell = 64;
    private int horizontal_lines = 8;
    private string[] vertical_lines = new[]{ "A", "B", "C", "D", "E", "F", "G", "H"};



    public void Generate()
    {
        for(int i = 1; i <= numCell; i++)
        {
            //get name of the cell
            var line_horizontal = Mathf.FloorToInt(i/8.1f) + 1;
            var live_vertical = vertical_lines[i-1 -((line_horizontal - 1)*horizontal_lines)];

            var cell = Instantiate(prefabCell, Vector3.zero, Quaternion.identity, this.transform);
            cell.gameObject.name = $"{live_vertical}{line_horizontal}";
        }
    } 
}
