using System;

namespace _22_Triangle
{
    class Program
    {
        static void Main()
        {
            int a, b, c;
            Triangle triangle;

            do
            {
                do
                {
                    InputValue("Введите сторону A: ", out a);
                    InputValue("Введите сторону B: ", out b);
                    InputValue("Введите сторону С: ", out c);

                    triangle = new Triangle(a, b, c);
                } while (triangle.IsNotCorrect());

                Console.WriteLine($"Стороны треугольника: A = {triangle.A}, B = {triangle.B}, C = {triangle.C}");
                Console.WriteLine($"Периметр треугольника равен: {triangle.Perimeter: #.###}");
                Console.WriteLine($"Площадь треугольника равна: {triangle.HeronArea: #.###}");

                Console.WriteLine("Начать заново? 1 - Да, 2 - Завершить программу.");
            } while (IsContinue());
        }


        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
        /// <returns>Возвращает bool-значение.</returns>
        static bool IsContinue()
        {
            while (true)
            {
                Console.Write("Ваш ввод: ");
                bool isParse = int.TryParse(Console.ReadLine(), out int value);

                if (isParse && value == 1)
                {
                    Console.Clear();
                    return true;
                }
                else if (isParse && value == 2) return false;
                else Console.WriteLine("Некорректный ввод, повторите ввод.");
            }
        }

        /// <summary>
        /// Ввод числовых данных с проверкой на корректность данных. 
        /// </summary>
        /// <param name="value">вводимое значение данных.</param>
        static void InputValue(string line, out int value)
        {
            Console.Write(line);

            while (true)
            {
                string str = Console.ReadLine();

                if (int.TryParse(str, out value) && value > 0)
                    break;
                else
                    Console.WriteLine("Ввод должен быть больше 0 либо натуральное положительное целое число.");

                Console.Write(line);
            }
        }
    }
}