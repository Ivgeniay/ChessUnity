using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Board board;
    private CellGenerator cellGenerator;
    private Black black;
    private White white;

    void Awake()
    {
        board = GetComponentInChildren<Board>();
        cellGenerator = GetComponentInChildren<CellGenerator>();
        black = gameObject.AddComponent<Black>();
        white = gameObject.AddComponent<White>();
    }
    void Start()
    {
        cellGenerator.Generate();
        black.StartPosition();
        white.StartPosition();
        Debug.Log(board.GetBoardPosition());
        
        
        board.SetBoardPosition("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        //board.StartPosition();
        //StartCoroutine(test());
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) Debug.Log(board.GetBoardPosition());
    }


}


public enum Role
{
    King,
    Queen,
    Rock,
    Bishop,
    Knight,
    pawn
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