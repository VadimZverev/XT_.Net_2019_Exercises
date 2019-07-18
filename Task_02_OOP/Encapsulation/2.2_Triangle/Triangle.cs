using System;

namespace _22_Triangle
{
    /// <summary>
    /// Пользовательская реализация треугольника
    /// </summary>
    class Triangle
    {
        public Triangle(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public int A { get; }
        public int B { get; }
        public int C { get; }

        // Площадь Герона: S = Sqrt(p(p-a)(p-b)(p-c)),
        //  где p - полу периметр p = 1/2 * (a + b + c).
        public double HeronArea
        {
            get
            {
                double semiPerimeter = Perimeter / 2;
                return Math.Sqrt(semiPerimeter * (semiPerimeter - A) * (semiPerimeter - B) * (semiPerimeter - C));
            }
        }

        public double Perimeter => A + B + C;

        /// <summary>
        /// Проверяет на корректность введённых сторон.
        /// </summary>
        /// <remarks>
        /// Если каждая из сторон не больше, чем сумма 2х других,
        /// то ввод корректен, иначе треугольник невозможно построить.
        /// </remarks>
        public bool IsCorrect()
        {
            return IsCorrect(A, B, C);
        }

        /// <summary>
        /// Проверяет на корректность введённых сторон.
        /// </summary>
        /// <remarks>
        /// Если каждая из сторон не больше, чем сумма 2х других,
        /// то ввод корректен, иначе треугольник невозможно построить.
        /// </remarks>
        public static bool IsCorrect(int a, int b, int c)
        {
            if (a >= b + c || b >= a + c || c >= a + b)
            {
                Console.WriteLine("Невозможно построить треугольник с текущими сторонами. Введите заново стороны.");
                return false;
            }
            else
                return true;
        }
    }
}
