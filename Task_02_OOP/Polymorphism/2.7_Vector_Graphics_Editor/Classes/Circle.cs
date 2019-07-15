using System;

namespace _27_Vector_Graphics_Editor.Classes
{
    class Circle : Figure, IDrawable
    {
        private double radius;

        /// <summary>
        /// Инициализирует новый экземпляр точки в начале координат.
        /// </summary>
        public Circle() : base() { }

        /// <summary>
        /// Инициализирует новый экземпляр в заданных координатах.
        /// </summary>
        /// <param name="x">ось X</param>
        /// <param name="y">ось Y</param>
        public Circle(double x, double y) : base(x, y) { }

        public double Radius
        {
            get => radius;
            set
            {
                if (value > 0)
                    radius = value;
                else
                    throw new ArgumentException("Назначаемое значение не может быть меньше, либо равно нулю.");
            }
        }

        public double Circumference => 2 * Math.PI * Radius;

        public void Draw()
        {
            Console.WriteLine("Нарисовать окружность.");
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return $"Тип: {typeof(Circle).Name}{Environment.NewLine}" +
                    $"Центр: {CentrePoint}{Environment.NewLine}" +
                    $"Радиус: {Radius}{Environment.NewLine}" +
                    $"Длина: {Circumference:#.###}";
        }
    }
}
