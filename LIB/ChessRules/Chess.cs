using System;



namespace ChessRules
{
    public class Chess
    {
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;

            ColorFigure color = new ColorFigure();
            this.squares = generateAllSquares();
            ArrangementOfFigures(fen);
        }
        public string fen { get; private set; }
        private Square[,] squares { get; set; }

        public bool MoveFigure(string _from, string _to)
        {
            var result = Validation(_from, _to);
            if (result == null) return false;

            return true;
        }
        public Figure GetFigure(int x, int y)
        {
            return squares[x, y].Figure;
        }

        public Figure GetFigure(string cellName)
        {
            //Square square = new Square(cellName);
            //int x = square.x;
            //int y = square.y;
            return Figure.none;
        }

        /*
        public string GetBoardPosition()
        {
            return 
        }
        */



        private string GenerateNewFen(Move move)
        {
            var fen = this.fen;
            var new_fen = "";

            return new_fen;
        }

        private Square[,] generateAllSquares()
        {
            Square[,] squares = new Square[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    squares[i, j] = new Square(i, j);
                }
            }

            return squares;
        }
        private void ArrangementOfFigures(string fen)
        {
            var array = parseString(fen);

            for (int i = 0; i < 8; i++)
            {
                var line = 7 - i;
                var code = convertCode(array[i]);
                createLineFigure(code, line);
            }
        }


        public static string[] parseString(string fen)
        {
            string[] parts = fen.Split('/');

            var str = parts[parts.Length - 1];

            string[] _parts = str.Split(' ');

            string[] result = new string[parts.Length + _parts.Length - 1];

            for (int i = 0; i < parts.Length; i++)
            {
                result[i] = parts[i];
            }
            for (int i = 0; i < _parts.Length; i++)
            {
                result[i + parts.Length - 1] = _parts[i];
            }


            return result;
        }

        private string convertCode(string code)
        {
            var result = "";

            if (code.Length == 8) return code;

            if (code.Length > 8) throw new System.Exception("Not support the code");

            if (code.Length < 8)
            {
                //Debug.Log("CODE:" + code);

                for (int i = 0; i < code.Length; i++)
                {
                    if (Char.IsNumber(code[i]))
                    {
                        int integerNum = code[i] - '0';

                        for (int j = 0; j < integerNum; j++)
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

        private void createLineFigure(string code, int line)
        {
            for(int i = 0; i < 8; i++)
            {
                Figure newFigure = Figure.none;

                if (code[i] == 'K')
                    newFigure = Figure.WhiteKing;
                if (code[i] == 'k')
                    newFigure = Figure.BlackKing;
                if (code[i] == 'B')
                    newFigure = Figure.WhiteBishop;
                if (code[i] == 'b')
                    newFigure = Figure.BlackBishop;
                if (code[i] == 'N')
                    newFigure = Figure.WhiteKnight;
                if (code[i] == 'n')
                    newFigure = Figure.BlackKnight;
                if (code[i] == 'Q')
                    newFigure = Figure.WhiteQueen;
                if (code[i] == 'q')
                    newFigure = Figure.BlackQueen;
                if (code[i] == 'R')
                    newFigure = Figure.WhiteRook;
                if (code[i] == 'r')
                    newFigure = Figure.BlackRook;
                if (code[i] == 'P')
                    newFigure = Figure.WhitePawn;
                if (code[i] == 'p')
                    newFigure = Figure.BlackPawn;
                
                squares[i, line].Figure = newFigure;

                //Console.WriteLine(squares[i, line].Figure);
            }
        }
        private Chess move(Move move)
        {
            
            return new Chess(fen);
        }

        private Move Validation(string _from, string _to)
        {
            if (_from.Length != 2) return null;
            if (_to.Length != 2) return null;
            if (_from.Equals(_to)) return null;

            _from = _from.ToUpper();
            _to = _to.ToUpper();

            string from = "";
            string to = "";

            for (int i = 0; i < Constant.vertical.Length; i++)
            {
                if (_from[0].ToString() == Constant.vertical[i]) from += _from[0].ToString();
                if (_to[0].ToString() == Constant.vertical[i].ToString()) to += _to[0].ToString();
            }
            for (int i = 0; i < Constant.horizontal.Length; i++)
            {
                if (_from[1].ToString() == Constant.horizontal[i].ToString()) from += _from[1].ToString();
                if (_to[1].ToString() == Constant.horizontal[i].ToString()) to += _to[1].ToString();
            }

            if (from.Length != 2) return null;
            if (to.Length != 2) return null;

            return new Move(from, to);
        }
    }
}