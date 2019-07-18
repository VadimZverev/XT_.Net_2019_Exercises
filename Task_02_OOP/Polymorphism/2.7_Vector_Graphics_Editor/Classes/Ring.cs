using System;

namespace Vector_Graphics_Editor.Classes
{
    /// <summary>
    /// Кольцо
    /// </summary>
    class Ring : Figure, IDrawable
    {
        private Round inner; //внутренний круг
        private Round outer; //внешний круг

        /// <summary>
        /// Инициализирует новый экземпляр точки в начале координат.
        /// </summary>
        public Ring(double inR, double outR)
            : this(inR, outR, 0, 0) { }

        /// <summary>
        /// Инициализирует новый экземпляр в заданных координатах.
        /// </summary>
        /// <param name="x">ось X</param>
        /// <param name="y">ось Y</param>
        public Ring(double inR, double outR, double x, double y)
            : base(x, y)
        {
            if (inR >= outR)
            {
                throw new ArgumentException("Внутренняя окружность не может быть "
                                            + "больше, либо равно внешней окружности");
            }

            inner = new Round { Radius = inR };
            outer = new Round { Radius = outR };
        }

        public double Area => outer.Area - inner.Area;
        public double Circumference
            => outer.Circumference + inner.Circumference;

        public void Draw()
        {
            Console.WriteLine("Нарисовать кольцо.");
            Console.WriteLine($"Тип: {typeof(Ring).Name}{Environment.NewLine}"
                              + $"Центр: {CentrePoint}{Environment.NewLine}"
                              + $"Площадь: {Area:#.###}{Environment.NewLine}"
                              + $"Суммарная длина окружностей: {Circumference:#.###}");
        }
    }
}
