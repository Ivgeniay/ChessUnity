namespace ChessRules
{
    public enum ColorFigure
    {
        White,
        Black
    }

    internal static class ColorFigureMethod
    {
        public static ColorFigure ColorFlip ( this ColorFigure colorFigure)
        {
            if (colorFigure == ColorFigure.Black) return ColorFigure.White; 
            else return ColorFigure.Black; 
        }
    }
}