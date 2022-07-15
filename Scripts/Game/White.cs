using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White : MonoBehaviour
{

    private Transform boardTransform;
    [SerializeField] private IEnumerable<FigureConfig> resources;
    private Dictionary<string, Figure> allFigures;
    private string[] vertical = new[]{ "A", "B", "C", "D", "E", "F", "G", "H"};

    void Awake()
    {
        boardTransform = GetComponentInChildren<Board>().transform;
        allFigures = new Dictionary<string, Figure>();
        resources = Resources.LoadAll<FigureConfig>("ScriptableObjects");
    }

    public void StartPosition()
    {
        ClearDictionary(allFigures);

        for (int i = 0; i < 8; i++)
        {
            CreateNewFigure($"{vertical[i]}2", "WhitePawn");
            CreateNewFigure($"{vertical[0]}1", "WhiteRook");
            CreateNewFigure($"{vertical[7]}1", "WhiteRook");
            CreateNewFigure($"{vertical[1]}1", "WhiteKnight");
            CreateNewFigure($"{vertical[6]}1", "WhiteKnight");
            CreateNewFigure($"{vertical[2]}1", "WhiteBishop");
            CreateNewFigure($"{vertical[5]}1", "WhiteBishop");
            CreateNewFigure($"{vertical[3]}1", "WhiteQueen");
            CreateNewFigure($"{vertical[4]}1", "WhiteKing");
        }
    }

    private void CreateNewFigure(string position, string figure)
    {
            var parentTransform = boardTransform.Find(position);
            if (parentTransform.childCount > 0) return;
            var newFigure = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity, parentTransform);
            var script = newFigure.AddComponent<Figure>();
            foreach (var e in resources)
            {
                if (e.name == figure) 
                {
                    script.Appoint(e);
                }
            }
            allFigures.Add(position, script);
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
