namespace _28_Game.General
{
    class Point
    {
        public int X { get; }
        public int Y { get; }

        /// <summary>
        /// Инициализирует новый экземпляр точки в начале координат.
        /// </summary>
        public Point() : this(0, 0) { }

        /// <summary>
        /// Инициализирует новый экземпляр в заданных координатах.
        /// </summary>
        /// <param name="x">ось X</param>
        /// <param name="y">ось Y</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }
    }
}
