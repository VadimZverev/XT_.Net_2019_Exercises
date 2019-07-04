using System;

namespace _14_XmasTree
{
    class Program
    {
        static void Main()
        {
            do
            {
                int number = InputPositiveValue();
                XmasTree(number);

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
        /// Отрисовывает ёлочку из треугольников.
        /// </summary>
        /// <param name="value">количество треугольников в ёлочке.</param>
        static void XmasTree(int value)
        {
            for (int k = 1; k <= value; k++)
            {
                for (int i = 1; i <= k; i++)
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
}
