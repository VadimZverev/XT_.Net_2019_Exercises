using System;

namespace _22_Triangle
{
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

        // Площадь Герона: S = Sqrt(p(p-a)(p-b)(p-c)), где p - полупериметр p = 1/2 * (a + b + c).
        public double HeronArea
        {
            get
            {
                double semiPerimeter = Perimeter / 2;
                return Math.Sqrt(semiPerimeter * (semiPerimeter - A) * (semiPerimeter - B) * (semiPerimeter - C));
            }
        }

        public double Perimeter => A + B + C;

        public bool IsNotCorrect()
        {
            if (A >= B + C || B >= A + C || C >= A + B)
            {
                Console.WriteLine("Невозможно построить треугольник с текущими сторонами. Введите заново стороны.");
                return true;
            }
            else
                return false;
        }
    }
}
