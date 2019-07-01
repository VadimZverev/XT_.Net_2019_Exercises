using System;

namespace _03_Square
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите положительное нечётное число: ");

                bool isParse = int.TryParse(Console.ReadLine(), out int number);

                if (isParse && number > 2 && (number % 2 != 0))
                    Square(number);
                else
                {
                    Console.WriteLine("Некорректный ввод(символы, отрицательные, чётные числа, 0 и 1 недопустимы)");
                    continue;
                }

                Console.WriteLine("Повторить ввод? 1 - Да, 2 - Выход из программы");
                while (true)
                {
                    Console.Write("Ваш ввод: ");
                    isParse = int.TryParse(Console.ReadLine(), out number);

                    if (isParse && number == 1) break;
                    else if (isParse && number == 2) return;
                    else Console.WriteLine("Некорректный ввод, повторите ввод.");
                }
            }
        }

        static void Square(int value)
        {
            for (int i = 1; i <= value; i++)
            {
                for (int j = 1; j <= value; j++)
                {
                    if ((value / 2 + 1 == i) && (value / 2 + 1 == j))
                        Console.Write(' ');
                    else
                        Console.Write('*');
                }
                Console.WriteLine();
            }
        }
    }
}
