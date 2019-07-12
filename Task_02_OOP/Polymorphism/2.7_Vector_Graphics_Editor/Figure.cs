namespace _27_Vector_Graphics_Editor
{
    abstract class Figure
    {
        /// <summary>
        /// Инициализирует новый экземпляр точки в начале координат.
        /// </summary>
        public Figure() : this(0, 0) { }

        /// <summary>
        /// Инициализирует новый экземпляр в заданных координатах.
        /// </summary>
        /// <param name="x">ось X</param>
        /// <param name="y">ось Y</param>
        public Figure(double x, double y)
        {
            CentrePoint = new Point(x, y);
        }

        public Point CentrePoint { get; set; }
    }
}