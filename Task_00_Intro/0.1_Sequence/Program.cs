using System;

namespace _01_Sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите число: ");
                bool isParse = int.TryParse(Console.ReadLine(), out int number);

                if (isParse && number > 0)
                    Sequence(number);
                else
                {
                    Console.WriteLine("Некорректный ввод, повторите попытку...");
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

        static void Sequence(int value)
        {
            Console.Write("Последовательность чисел: ");
            for (int i = 1; i <= value; i++)
            {
                Console.Write(i);
                if (i != value)
                    Console.Write(", ");
            }
            Console.WriteLine('.');
        }
    }
}
