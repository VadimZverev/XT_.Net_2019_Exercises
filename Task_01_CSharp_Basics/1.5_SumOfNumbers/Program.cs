using System;

namespace _15_SumOfNumbers
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine($"The sum of integers divided by 3 and 5 is equal to: {Sum()}");
        }

        /// <summary>
        /// Returns the sum of integers divided by 3 and 5.
        /// </summary>
        private static int Sum()
        {
            int result = 0;

            for (int i = 1; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    result += i;
                }
            }

            return result;
        }
    }
}
