using System;

namespace _12_Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите положительное число: ");

                bool isParse = int.TryParse(Console.ReadLine(), out int number);

                if (isParse && number > 0)
                    Triangle(number);
                else
                {
                    Console.WriteLine("Некорректный ввод(символы, отрицательные, 0 недопустимы)");
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

        static void Triangle(int value)
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
