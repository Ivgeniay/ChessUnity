

public class Fen
{
    private Board _board;
    public Fen (Board board)
    {
        this._board = board;
    }

    public string StartPosition()
    {
        return "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
    }

    private string getBoardPosition {
        get => _board.GetBoardPosition();
    }
    
}

interface IPosition
{

}