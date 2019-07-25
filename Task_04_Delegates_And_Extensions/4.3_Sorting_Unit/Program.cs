using System;
using System.Threading;

namespace _43_Sorting_Unit
{
    class Program
    {
        static void Main()
        {
            SortingUnit.OnComplete += Complete;

            int[] arr = { 1, 5, 10, 8, 100, 2, 14, 0, 3, -100, -98, -1000 };

            SortingUnit.SortAsync(arr, (a, b) => a > b);
            SortingUnit.SortAsync(arr, (a, b) => a > b);
            SortingUnit.SortAsync(arr, (a, b) => a > b);
            SortingUnit.SortAsync(arr, (a, b) => a > b);
            SortingUnit.SortAsync(arr, (a, b) => a > b);
        }

        static void Complete(string message)
        {
            Console.WriteLine(message);
        }
    }
}
