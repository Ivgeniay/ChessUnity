using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black : MonoBehaviour
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
            CreateNewFigure($"{vertical[i]}7", "BlackPawn");
            CreateNewFigure($"{vertical[0]}8", "BlackRook");
            CreateNewFigure($"{vertical[7]}8", "BlackRook");
            CreateNewFigure($"{vertical[1]}8", "BlackKnight");
            CreateNewFigure($"{vertical[6]}8", "BlackKnight");
            CreateNewFigure($"{vertical[2]}8", "BlackBishop");
            CreateNewFigure($"{vertical[5]}8", "BlackBishop");
            CreateNewFigure($"{vertical[3]}8", "BlackQueen");
            CreateNewFigure($"{vertical[4]}8", "BlackKing");

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
