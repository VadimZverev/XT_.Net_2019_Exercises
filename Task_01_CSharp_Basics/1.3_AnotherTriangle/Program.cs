using System;

namespace _13_AnotherTriangle
{
    class Program
    {
        static void Main()
        {
            do
            {
                int number = InputPositiveValue();
                DrawAnotherTriangle(number);

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
        static void DrawAnotherTriangle(int value)
        {
            for (int i = 1; i <= value; i++)
            {
                for (int j = 1; j < value + i; j++)
                {
                    if (j <= value - i)
                        Console.Write(' ');
                    else
                        Console.Write('*');
                }

                Console.WriteLine();
            }
        }
    }
}
