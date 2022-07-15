using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

//Board keep statement
public class Board : MonoBehaviour
{
    private string[] vertical = new[]{ "A", "B", "C", "D", "E", "F", "G", "H"};

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
            case Role.Rock:
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

    //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
    public void SetBoardPosition(string position)
    {
        foreach(string e in parseString(position))
        {
            Debug.Log(e);
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


}
