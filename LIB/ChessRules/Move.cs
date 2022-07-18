namespace ChessRules
{
    internal class Move
    {
        public Move(string from, string to)
        {
            this.from = from;
            this.to = to;
        }

        private string from;
        private string to;

        public void Print()
        {
            Console.WriteLine($"from: {from}, to {to}");
        }
    }
}