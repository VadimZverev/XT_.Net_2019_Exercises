using System;

namespace _02_Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите число (от 2 и выше): ");
                bool isParse = int.TryParse(Console.ReadLine(), out int number);

                if (isParse && number > 0 && number != 1)
                {
                    Simple(number);
                }
                else
                {
                    Console.WriteLine("Некорректный ввод(символы, отрицательные числа, 0 и 1 недопустимы)");
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

        static void Simple(int value)
        {
            int count = 0;

            for (int i = 1; i <= value; i++)
            {
                if (value % i == 0)
                    count++;

                if (count > 2) break;
            }

            if (count > 2)
                Console.WriteLine("Число является составным");
            else
                Console.WriteLine("Число является простым");
        }
    }
}
