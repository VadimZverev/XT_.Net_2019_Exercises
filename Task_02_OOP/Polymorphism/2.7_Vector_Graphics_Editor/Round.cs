﻿using System;

namespace _27_Vector_Graphics_Editor
{
    class Round : Figure, IDrawable
    {
        private double radius;

        /// <summary>
        /// Инициализирует новый экземпляр точки в начале координат.
        /// </summary>
        public Round() : base() { }

        /// <summary>
        /// Инициализирует новый экземпляр в заданных координатах.
        /// </summary>
        /// <param name="x">ось X</param>
        /// <param name="y">ось Y</param>
        public Round(int x, int y) : base(x, y) { }

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

        public double Area => Math.PI * Radius * Radius;
        public double Circumference => 2 * Math.PI * Radius;

        public void Draw()
        {
            Console.WriteLine("Нарисовать круг.");
            Console.WriteLine(this);

        }

        public override string ToString()
        {
            return $"Тип: {typeof(Round).Name}{Environment.NewLine}" +
                    $"Центр: {CentrePoint}{Environment.NewLine}" +
                    $"Радиус: {Radius}{Environment.NewLine}" +
                    $"Площадь: {Area:#.###}{Environment.NewLine}" +
                    $"Длина: {Circumference:#.###}";
        }
    }
}
