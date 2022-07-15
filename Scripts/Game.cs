using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Board board;
    private CellGenerator cellGenerator;

    void Awake()
    {
        board = GetComponentInChildren<Board>();
        cellGenerator = GetComponentInChildren<CellGenerator>();

    }
    void Start()
    {
        cellGenerator.Generate();
        StartCoroutine(test());
        //board.StartPosition();
    }

    private IEnumerator test()
    {
        yield return new WaitForSeconds(1);
        board.StartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
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