using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Board board;
    private ResourcesChess resourcesChess;
    private CellGenerator cellGenerator;
    private Fen fen;

    void Awake()
    {
        resourcesChess = gameObject.AddComponent<ResourcesChess>();
        cellGenerator = GetComponentInChildren<CellGenerator>();
        board = GetComponentInChildren<Board>();
        fen = new Fen(board);
    }
    void Start()
    {
        cellGenerator.Generate();
        board.SetBoardPosition(fen.StartPosition());
    }

    private void OnActionFigureLiftedHandler(Figure obj)
    {
        //Debug.Log(obj.name + " from " + obj.transform.parent.name);
    }

    private void OnActionFigureWasPlacedHandler(Figure obj)
    {
        //Debug.Log(obj.name + " to " + obj.transform.parent.name);
    }

  private void Update() 
    {
        if (Input.GetKeyUp(KeyCode.Space)) Debug.Log(board.GetBoardPosition());
        if (Input.GetKeyUp(KeyCode.UpArrow)) board.SetBoardPosition("rnbqkbnr/pp3ppp/2p1p3/3p4/3P4/2P1P3/PP3PPP/RNBQKBNR hjhgljg hjvhj");
        //if (Input.GetKeyUp(KeyCode.UpArrow)) board.SetBoardPosition("rnb1kb1r/pppp1ppp/4p3/8/1P5q/P4P1P/2PP3P/R1BQKB1R");
    }

    void OnDisable()
    {
        cellGenerator.OnActionFigureWasPlaced -= OnActionFigureWasPlacedHandler;
        board.OnActionFigureLifted -= OnActionFigureLiftedHandler;
    }

    void OnEnable()
    {
        cellGenerator.OnActionFigureWasPlaced += OnActionFigureWasPlacedHandler;
        board.OnActionFigureLifted += OnActionFigureLiftedHandler;
    }


}

public enum CellName
{
    A1, A2, A3, A4, A5, A6, A7, A8,
    B1, B2, B3, B4, B5, B6, B7, B8,
    C1, C2, C3, C4, C5, C6, C7, C8,
    D1, D2, D3, D4, D5, D6, D7, D8,
    E1, E2, E3, E4, E5, E6, E7, E8,
    F1, F2, F3, F4, F5, F6, F7, F8,
    G1, G2, G3, G4, G5, G6, G7, G8,
    H1, H2, H3, H4, H5, H6, H7, H8
}