namespace _28_Game.General
{
    class GameField
    {
        public GameField() : this(height: 150, width: 300) { }

        public GameField(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public int Height { get; }
        public int Width { get; }

        public static GameField CreateGameField(int height, int width)
            => new GameField(height, width);
    }
}
