﻿namespace Vector_Graphics_Editor.Classes
{
    class Point
    {
        /// <summary>
        /// Инициализирует новый экземпляр точки в начале координат.
        /// </summary>
        public Point()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Инициализирует новый экземпляр в заданных координатах.
        /// </summary>
        /// <param name="x">ось X</param>
        /// <param name="y">ось Y</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public override string ToString()
        {
            return $"({X};{Y})";
        }
    }
}
