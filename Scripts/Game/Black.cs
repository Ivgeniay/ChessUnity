using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black : MonoBehaviour
{
    private Transform boardTransform;
    private ResourcesChess resources;
    private Dictionary<string, Figure> allFigures;
    private string[] vertical = new[]{ "A", "B", "C", "D", "E", "F", "G", "H"};

    void Awake()
    {
        boardTransform = GetComponentInChildren<Board>().transform;
        allFigures = new Dictionary<string, Figure>();
        resources = GetComponent<ResourcesChess>();
    }

    public void StartPosition()
    {
        ClearDictionary(allFigures);

        for (int i = 0; i < 8; i++)
        {
            CreateNewFigure.New($"{vertical[i]}7", "BlackPawn");
            CreateNewFigure.New($"{vertical[0]}8", "BlackRook");
            CreateNewFigure.New($"{vertical[7]}8", "BlackRook");
            CreateNewFigure.New($"{vertical[1]}8", "BlackKnight");
            CreateNewFigure.New($"{vertical[6]}8", "BlackKnight");
            CreateNewFigure.New($"{vertical[2]}8", "BlackBishop");
            CreateNewFigure.New($"{vertical[5]}8", "BlackBishop");
            CreateNewFigure.New($"{vertical[3]}8", "BlackQueen");
            CreateNewFigure.New($"{vertical[4]}8", "BlackKing");

        }
    }

    private void ClearDictionary(Dictionary<string, Figure> dictionary)
    {
        foreach (KeyValuePair<string, Figure> e in  dictionary)
        {
            Destroy(e.Value.gameObject);
        }
        dictionary.Clear();
    }
}
