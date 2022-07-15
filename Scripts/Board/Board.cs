using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

//Board keep statement
public class Board : MonoBehaviour
{
    private string[] vertical = new[]{ "A", "B", "C", "D", "E", "F", "G", "H"};
    public ResourcesChess resources;

    void Start()
    {
        resources = GameObject.Find("[GAME]").GetComponent<ResourcesChess>();
    }

    public string GetBoardPosition()
    {
        string report = "";        

        for(int i = 8; i >= 1; i--)
        {
            int counter = 0;

            for(int j = 0; j < 8; j++)
            {
                var children = transform.Find($"{vertical[j]}{i}");
                if (children.childCount > 0)
                {
                    string str = "";
                    if (counter > 0) str = counter.ToString();
                    report += str+separate(children.GetComponentInChildren<Figure>().figureConfig, children.name);
                    counter = 0;
                }
                else
                {
                    counter++;
                }

            }

            if (counter > 0) report += counter.ToString() + "/";
            else if (i != 1) report += "/";
        }

        return report;
    }

    private string separate(FigureConfig conf, string CellName)
    {
        string report = "";

        switch (conf.Role)
        {
            case Role.King:
                report = "K";
                break;
            case Role.Bishop:
                report = "B";
                break;
            case Role.Knight:
                report = "N";
                break;
            case Role.Queen:
                report = "Q";
                break;
            case Role.Rook:
                report = "R";
                break;
            case Role.pawn:
                report = "P";
                break;
        }

        if (conf.Color == "Black") 
            report = report.ToLower();
        else
            report = report.ToUpper();

        return report;
    }
    public void SetBoardPosition(string position)
    {
        deleteAllFigures();
        var array = parseString(position);

        //vertical
        for (int i = 0; i < 8; i++)
        {
            createLineFigure(array[i], i+1); 
        }
        
    }
    private string[] parseString(string position)
    {
        string[] parts = position.Split('/');

        var str = parts[parts.Length-1];

        string[] _parts = str.Split(' ');

        string[] result = new string[parts.Length + _parts.Length - 1];

        for (int i = 0; i < parts.Length; i++)
        {
            result[i] = parts [i];
        }
        for (int i = 0; i < _parts.Length; i++)
        {
            result[i + parts.Length-1] = _parts [i];
        }


        return result;
    }
    private void deleteAllFigures()
    {
        for(int i = 1; i <= 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                var children = transform.Find($"{vertical[j]}{i}");
                if (children.childCount > 0)
                    Destroy(children.GetComponentInChildren<Figure>().gameObject);
            }
        }
    }

    //horizontall
    private void createLineFigure(string code, int line)
    {
        if(code.Length == 8)
        {
            for(int i = 0; i < code.Length; i++)
            {
                if (code[i] == 'K') 
                    CreateNewFigure($"{vertical[i]}{line}", "WhiteKing");
                if (code[i] == 'k')
                    CreateNewFigure($"{vertical[i]}{line}", "BlackKing");
                if (code[i] == 'B') 
                    CreateNewFigure($"{vertical[i]}{line}", "WhiteBishop");
                if (code[i] == 'b')
                    CreateNewFigure($"{vertical[i]}{line}", "BlackBishop");
                if (code[i] == 'N') 
                    CreateNewFigure($"{vertical[i]}{line}", "WhiteKnight");
                if (code[i] == 'n')
                    CreateNewFigure($"{vertical[i]}{line}", "BlackKnight");
                if (code[i] == 'Q') 
                    CreateNewFigure($"{vertical[i]}{line}", "WhiteQueen");
                if (code[i] == 'q')
                    CreateNewFigure($"{vertical[i]}{line}", "BlackQueen");
                if (code[i] == 'R') 
                    CreateNewFigure($"{vertical[i]}{line}", "WhiteRook");
                if (code[i] == 'r')
                    CreateNewFigure($"{vertical[i]}{line}", "BlackRook");
                if (code[i] == 'P') 
                    CreateNewFigure($"{vertical[i]}{line}", "WhitePawn");
                if (code[i] == 'p')
                    CreateNewFigure($"{vertical[i]}{line}", "BlackPawn");
            }
        }
    }

    private void CreateNewFigure(string position, string figure)
    {
        Debug.Log(position + " " + figure);
        var parentTransform = transform.Find(position);
        var newFigure = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity, parentTransform);
        var script = newFigure.AddComponent<Figure>();
        foreach (var e in resources.GetResourses())
        {
            if (e.name == figure) 
            {
                script.Appoint(e);
            }
        }
    }



}
