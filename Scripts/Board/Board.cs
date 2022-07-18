using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

//Board keep statement
public class Board : MonoBehaviour
{
    public event Action<Figure> OnActionFigureLifted;
    private ResourcesChess resources;
    public List<Figure> figureList;

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
                var children = transform.Find($"{Constants.vertical[j]}{i}");
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
    public void SetBoardPosition(string position)
    {
        deleteAllFigures();
        StartCoroutine(setBoardPosition(position));
    }


    private IEnumerator setBoardPosition(string position)
    {
        yield return null;

        var array = parseString(position);

        for (int i = 0; i < 8; i++)
        {
            var line = 8-i;
            var code = convertCode(array[i]);
            createLineFigure(code, line); 
        }
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

        if (conf.Color == ColorList.Black) 
            report = report.ToLower();
        else
            report = report.ToUpper();

        // report = conf.Role.ToString() ;
        // Debug.Log(report);

        // if (conf.Color == ColorList.Black)
        //     report = report.ToLower();


        return report;
    }
    //parse code rnbqkbnr/pppppppp/8/PPPPP1P1/8/8/P1P5/RNBQKBNR to array
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
    //parce code 2p2p2 to 11p11p11
    private string convertCode(string code)
    {
        var result = "";

        if (code.Length == 8) return code;

        if (code.Length > 8) throw new System.Exception ("Not support the code");

        if (code.Length < 8)
        {
            //Debug.Log("CODE:" + code);

            for(int i = 0; i < code.Length; i++)
            {
                if (Char.IsNumber(code[i]))
                {
                    int integerNum = code[i] - '0';

                    for(int j = 0; j < integerNum; j++)
                    {
                        result += 1;
                    }
                }
                else
                {
                    result += code[i];
                }
            }
        }

        return result;
    }
    private void deleteAllFigures()
    {
        foreach(Transform child in transform)
        {
            foreach (Transform childchild in child)
            {
                Debug.Log(childchild.name + " is destroed");
                Destroy(childchild.gameObject);
            }
        }

        foreach(Figure e in figureList)
        {
            e.OnActionFigureLifted -= OnActionFigureLiftedHandler;
        }
        figureList.Clear();
    }
    //horizontall
    private void createLineFigure(string code, int line)
    {
        for(int i = 0; i < code.Length; i++)
        {
            if (code[i] == 'K') 
                createFig($"{Constants.vertical[i]}{line}", "WhiteKing");
            if (code[i] == 'k')
                createFig($"{Constants.vertical[i]}{line}", "BlackKing");
            if (code[i] == 'B') 
                createFig($"{Constants.vertical[i]}{line}", "WhiteBishop");
            if (code[i] == 'b')
                createFig($"{Constants.vertical[i]}{line}", "BlackBishop");
            if (code[i] == 'N') 
                createFig($"{Constants.vertical[i]}{line}", "WhiteKnight");
            if (code[i] == 'n')
                createFig($"{Constants.vertical[i]}{line}", "BlackKnight");
            if (code[i] == 'Q') 
                createFig($"{Constants.vertical[i]}{line}", "WhiteQueen");
            if (code[i] == 'q')
                createFig($"{Constants.vertical[i]}{line}", "BlackQueen");
            if (code[i] == 'R') 
                createFig($"{Constants.vertical[i]}{line}", "WhiteRook");
            if (code[i] == 'r')
                createFig($"{Constants.vertical[i]}{line}", "BlackRook");
            if (code[i] == 'P') 
                createFig($"{Constants.vertical[i]}{line}", "WhitePawn");
            if (code[i] == 'p')
                createFig($"{Constants.vertical[i]}{line}", "BlackPawn");
        }

    }
    public void createFig(string position, string figure)
    {
        var newfig = CreateNewFigure.New(position, figure);
        var figureScr = newfig.GetComponent<Figure>();
        figureScr.OnActionFigureLifted += OnActionFigureLiftedHandler;
        figureList.Add(figureScr);

    }
    private void deleteFig(Figure figure)
    {
        Destroy(figure.gameObject);
        figure.OnActionFigureLifted -= OnActionFigureLiftedHandler;
        figureList.Remove(figure);
    }
    private void OnActionFigureLiftedHandler(Figure obj)
    {
        OnActionFigureLifted?.Invoke(obj);
    }


}
