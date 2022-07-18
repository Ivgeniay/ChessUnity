namespace ChessRules
{
    internal class Square
    {
        public Figure Figure { get; set; }
        public int x { get; private set; }
        public int y { get; private set; }
        public Square(int x, int y)
        {
            Figure = Figure.none;
            this.x = x;
            this.y = y;
        }
        public Square(string cellName)
        {
            cellName = cellName.ToLower();
            if (cellName.Length == 2 &&
                cellName[0] >= 'a' && cellName[0] <= 'h' &&
                cellName[1] >= '1' && cellName[1] <= '8')
            {
                x = cellName[0] - 'a' + 1;          // 'a' - 'a' = 0; 'b' - 'a' = 1
                y = cellName[1] - '1';              // 1 - 1 = 0; 2- 1 = 1;
            }
            else
            {
                this.x = -1;
                this.y = -1;
            }
        }

        public string GetCellName
        {
            get
            {
                return ((char)('a' + x)).ToString() + (y + 1).ToString();
            }
        }

        public bool OnBoard(int x, int y)
        {
            return (x >= 0 && x < 8) && (y >= 0 && x <8);
        }

    }


}