using System;

namespace Vector_Graphics_Editor.Classes
{
    /// <summary>
    /// Линия(Отрезок)
    /// </summary>
    class Line : Figure, IDrawable
    {
        /// <summary>
        /// Инициализирует экземпляр с заданием начальной и конечной точек.
        /// </summary>
        /// <param name="startPoint">начальная точка</param>
        /// <param name="endPoint">конечная точка</param>
        public Line(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            CentrePoint = GetCentrePoint();
            Length = GetLength();
        }

        #region Свойства

        public double Length { get; }
        public Point StartPoint { get; }
        public override Point CentrePoint { get; }
        public Point EndPoint { get; }

        #endregion

        #region Методы

        public void Draw()
        {
            Console.WriteLine("Нарисовать линию.");
            Console.WriteLine($"Тип: {nameof(Line)}{Environment.NewLine}"
                              + $"Начальная точка: {StartPoint}.{Environment.NewLine}"
                              + $"Точка середины линии: {CentrePoint}.{Environment.NewLine}"
                              + $"Конечная точка: {EndPoint}.{Environment.NewLine}"
                              + $"Длина: {Length:#.###}");
        }

        private Point GetCentrePoint()
        {
            double x = (StartPoint.X + EndPoint.X) / 2;
            double y = (StartPoint.Y + EndPoint.Y) / 2;

            return new Point(x, y);
        }

        private double GetLength()
        {
            double a = EndPoint.X - StartPoint.X;
            double b = EndPoint.Y - StartPoint.Y;

            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        } 

        #endregion
    }
}
