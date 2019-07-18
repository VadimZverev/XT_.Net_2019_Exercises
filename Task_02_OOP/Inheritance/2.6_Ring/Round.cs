using System;

namespace _26_Ring
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

        /// <summary>
        /// Радиус. Не может быть меньше либо равен нулю.
        /// </summary>
        /// <exception cref="ArgumentException">Если равен либо меньше 0.</exception>
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
