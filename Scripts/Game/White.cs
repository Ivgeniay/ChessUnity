using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White : MonoBehaviour
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
            CreateNewFigure.New($"{vertical[i]}2", "WhitePawn");
            CreateNewFigure.New($"{vertical[0]}1", "WhiteRook");
            CreateNewFigure.New($"{vertical[7]}1", "WhiteRook");
            CreateNewFigure.New($"{vertical[1]}1", "WhiteKnight");
            CreateNewFigure.New($"{vertical[6]}1", "WhiteKnight");
            CreateNewFigure.New($"{vertical[2]}1", "WhiteBishop");
            CreateNewFigure.New($"{vertical[5]}1", "WhiteBishop");
            CreateNewFigure.New($"{vertical[3]}1", "WhiteQueen");
            CreateNewFigure.New($"{vertical[4]}1", "WhiteKing");
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
