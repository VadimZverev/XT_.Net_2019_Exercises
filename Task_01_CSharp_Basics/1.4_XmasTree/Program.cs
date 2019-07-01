using System;

namespace _14_XmasTree
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
                    XmasTree(number);
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
