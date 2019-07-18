using System;

namespace Vector_Graphics_Editor.Classes
{
    /// <summary>
    /// Прямоугольник
    /// </summary>
    class Rectangle : Figure, IDrawable
    {
        private double height;
        private double width;

        /// <summary>
        /// Инициализирует новый экземпляр точки в начале координат.
        /// </summary>
        public Rectangle(double a, double b)
            : this(a, b, 0, 0) { }

        /// <summary>
        /// Инициализирует новый экземпляр в заданных координатах.
        /// </summary>
        /// <param name="height">высота</param>
        /// <param name="width">ширина</param>
        /// <param name="x">ось X</param>
        /// <param name="y">ось Y</param>
        public Rectangle(double height, double width, double x, double y) 
            : base(x, y)
        {
            Height = height;
            Width = width;
        }

        #region Свойства

        public double Area => height * width;
        public double Diagonal
            => Math.Sqrt(Math.Pow(height, 2) + Math.Pow(width, 2));
        public double Height
        {
            get => height;
            set
            {
                if (value > 0)
                {
                    height = value;
                }
                else
                {
                    throw new ArgumentException("Назначаемое значение не может "
                                                + "быть меньше, либо равно нулю.");
                }
            }
        }

        public double Perimeter => 2 * (height + width);

        public double Width
        {
            get => width;
            set
            {
                if (value > 0)
                {
                    width = value;
                }
                else
                {
                    throw new ArgumentException("Назначаемое значение не может "
                                                + "быть меньше, либо равно нулю.");
                }
            }
        }

        #endregion

        public void Draw()
        {
            Console.WriteLine("Нарисовать прямоугольник.");
            Console.WriteLine($"Тип: {typeof(Rectangle).Name}{Environment.NewLine}"
                              + $"Центр: {CentrePoint}{Environment.NewLine}"
                              + $"Высота: {Height}{Environment.NewLine}"
                              + $"Ширина: {Width}{Environment.NewLine}"
                              + $"Площадь: {Area:#.###}{Environment.NewLine}"
                              + $"Диагональ: {Diagonal:#.###}{Environment.NewLine}"
                              + $"Периметр: {Perimeter:#.###}");
        }
    }
}
