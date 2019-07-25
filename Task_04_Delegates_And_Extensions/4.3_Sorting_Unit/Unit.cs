using System;
using System.Threading;

namespace _43_Sorting_Unit
{
    static class SortingUnit
    {
        public static event Action<string> OnComplete;

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

        /// <summary>
        /// Sorting an arbitrary array in ascending order in a separate thread.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="T">array type</typeparam>
        /// <param name="array">array to be sorted</param>
        /// <param name="compare">comparison delegate</param>
        public static void SortAsync<T>(T[] array, Func<T, T, bool> compare)
        {
            Thread thrd = new Thread(() =>
            {
                Console.WriteLine($"Starting Thread #{Thread.CurrentThread.ManagedThreadId}");

                Sort(array, compare);

                Console.WriteLine($"Do Some .... in Thread #{Thread.CurrentThread.ManagedThreadId}");
                //Thread.Sleep(TimeSpan.FromSeconds(1.0));

                OnComplete?.Invoke($"Sort is done in Thread #{Thread.CurrentThread.ManagedThreadId}.");
            });

            thrd.Start();
        }
    }
}
