using System;

namespace _15_SumOfNumbers
{
    class Program
    {
        static void Main()
        {
            Sum();
        }

        private static void Sum()
        {
            int result = 0;

            for (int i = 1; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    result += i;
                }
            }

            Console.WriteLine($"Сумма целых чисел, делимая на 3 и 5 без остатка равна: {result}");
        }
    }
}
