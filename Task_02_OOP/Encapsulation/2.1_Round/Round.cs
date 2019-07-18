using System;

namespace _21_Round
{
    /// <summary>
    /// Является пользовательским кругом.
    /// </summary>
    class Round
    {
        private int radius;
        private Point point;

        public Round()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Ось Х.
        /// </summary>
        public int X
        {
            get => point.x;
            set => point.x = value;
        }

        /// <summary>
        /// Ось У.
        /// </summary>
        public int Y { get; set; }
        public int Radius
        {
            get => radius;
            set
            {
                if (value > 0)
                    radius = value;
                else
                    throw new ArgumentException("Радиус не может быть меньше, либо равно нулю.");
            }
        }

        /// <summary>
        /// Возвращает площадь круга.
        /// </summary>
        public double Area => Math.PI * Radius * Radius;

        /// <summary>
        /// Возвращает длину описанной окружности.
        /// </summary>
        public double Circumference => 2 * Math.PI * Radius;

        /// <summary>
        /// Точка центра круга.
        /// </summary>
        private struct Point
        {
            public int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
