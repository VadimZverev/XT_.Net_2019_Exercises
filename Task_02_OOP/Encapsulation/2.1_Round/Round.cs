using System;

namespace _21_Round
{
    class Round
    {
        private int radius;

        public Round()
        {
            X = 0;
            Y = 0;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Radius
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

        public double Area => Math.PI * Radius * Radius;
        public double Circumference => 2 * Math.PI * Radius;
    }
}
