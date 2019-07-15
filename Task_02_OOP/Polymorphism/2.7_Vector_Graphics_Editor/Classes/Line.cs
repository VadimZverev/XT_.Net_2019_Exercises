using System;

namespace _27_Vector_Graphics_Editor.Classes
{
    class Line : Figure, IDrawable
    {
        public double Length { get; }
        public Point StartPoint { get; }
        public new Point CentrePoint { get; }
        public Point EndPoint { get; }

        public Line(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            CentrePoint = GetCentrePoint();
            Length = GetLength();
        }

        public void Draw()
        {
            Console.WriteLine("Нарисовать линию.");
            Console.WriteLine(this);
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

        public override string ToString()
        {
            return $"Тип: {typeof(Line).Name}{Environment.NewLine}" +
                    $"Начальная точка: {StartPoint}.{Environment.NewLine}" +
                    $"Точка середины линии: {CentrePoint}.{Environment.NewLine}" +
                    $"Конечная точка: {EndPoint}.{Environment.NewLine}" +
                    $"Длина: {Length:#.###}{Environment.NewLine}";
        }
    }
}
