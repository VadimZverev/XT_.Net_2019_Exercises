using System;

namespace _26_Ring
{
    class Ring
    {
        private Round inner; //внутренняя окружность
        private Round outer; //внешняя окружность

        /// <summary>
        /// Инициализирует кольцо с заданными параметрами.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Если внутренняя окружность равна, либо больше внешней окружности.
        /// </exception>
        /// <param name="inR">внутренний радиус</param>
        /// <param name="outR">внешний радиус</param>
        /// <param name="x">Ось Х</param>
        /// <param name="y">Ось У</param>
        public Ring(int inR, int outR, int x, int y)
        {
            if (inR >= outR)
            {
                throw new ArgumentException("Внутренняя окружность не может быть "
                                            + "больше либо равно внешней окружности");
            }

            inner = new Round { Radius = inR, X = x, Y = y };
            outer = new Round { Radius = outR, X = x, Y = y };
        }

        /// <summary>
        /// Площадь кольца.
        /// </summary>
        public double Area => outer.Area - inner.Area;

        /// <summary>
        /// Суммарная длина внешней и внутренней окружности кольца.
        /// </summary>
        public double Circumference
            => outer.Circumference + inner.Circumference;
        public int X => outer.X;
        public int Y => outer.Y;
    }
}
