using System;

namespace _26_Ring
{
    class Ring
    {
        private Round inner; //внутренний круг
        private Round outer; //внешний круг

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

        public double Area => outer.Area - inner.Area;
        public double Circumference
            => outer.Circumference + inner.Circumference;
        public int X => outer.X;
        public int Y => outer.Y;
    }
}
