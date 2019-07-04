using System;

namespace _12_Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                int number = InputPositiveValue();
                DrawTriangle(number);

                Console.WriteLine("Начать заново? 1 - Да, 2 - Выход из программы");
            } while (IsContinue());
        }

        /// <summary>
        /// Возвращает положительное целое число.
        /// </summary>
        static int InputPositiveValue()
        {
            while (true)
            {
                Console.Write("Введите положительное число: ");
                bool isParse = int.TryParse(Console.ReadLine(), out int number);

                if (isParse && number > 0) return number;
                else
                {
                    Console.WriteLine("Некорректный ввод(символы, отрицательные, 0 недопустимы)");
                    continue;
                }
            }
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
        /// Отрисовывает треугольник.
        /// </summary>
        /// <param name="value">высота треугольника.</param>
        static void DrawTriangle(int value)
        {
            for (int i = 1; i <= value; i++)
            {
                for (int j = 1; j <= i; j++)
                    Console.Write('*');

                Console.WriteLine();
            }
        }
    }
}
