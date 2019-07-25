using System;

namespace _41_Custom_Sort
{
    class Program
    {
        static void Main()
        {
            int[] arr = { 1, 5, 10, 8, 100, 2, 14, 0, 3, -100, -98, -1000 };

            Console.WriteLine("Массив до сортировки:");

            foreach (var item in arr)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine();

            Sort(arr, (a, b) => a > b);

            Console.WriteLine("Массив после сортировки:");

            foreach (var item in arr)
            {
                Console.Write($"{item} ");
            }
        }

        /// <summary>
        /// Sort an arbitrary array in ascending order.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="T">array type</typeparam>
        /// <param name="array">array to be sorted</param>
        /// <param name="compare">comparison delegate</param>
        public static void Sort<T>(T[] array, Func<T, T, bool> compare)
        {
            if (compare == null)
            {
                throw new ArgumentNullException();
            }

            // Избыточность цикла(<=) пришлось оставить по причине того,
            // что в случае одинаковой длинны слов он несколько слов
            // не устанавливал правильно в алфавитном порядке.
            for (int i = 0; i <= array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (compare(array[j], array[j + 1]))
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }
}
