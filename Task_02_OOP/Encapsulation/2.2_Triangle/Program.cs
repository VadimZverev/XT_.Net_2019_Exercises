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

                } while (!Triangle.IsCorrect(a, b, c));

                triangle = new Triangle(a, b, c);

                Console.WriteLine($"Стороны треугольника: A = {triangle.A}, B = {triangle.B}, C = {triangle.C}");
                Console.WriteLine($"Периметр треугольника равен: {triangle.Perimeter: #.###}");
                Console.WriteLine($"Площадь треугольника равна: {triangle.HeronArea: #.###}");

            } while (IsContinue());
        }


        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
        static bool IsContinue()
        {
            Console.WriteLine("Начать заново? 1 - Да, 2 - Завершить программу.");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        return true;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return false;
                }
            }
        }

        /// <summary>
        /// Ввод числовых данных с проверкой на корректность данных. 
        /// </summary>
        /// <param name="line">какая сторона вводится.</param>
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