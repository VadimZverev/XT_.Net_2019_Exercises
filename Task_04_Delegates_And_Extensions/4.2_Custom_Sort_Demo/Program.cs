using System;

namespace _42_Custom_Sort_Demo
{
    class Program
    {
        static void Main()
        {
            string[] arr = { "begun", "one", "begin", "two", "three", "four", "five", "six", "began" };

            Console.WriteLine("Массив до сортировки:");

            foreach (var item in arr)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine(Environment.NewLine);

            #region Вариант 1

            Sort(arr, Compare);

            #endregion

            #region Вариант 2

            //Sort(arr, (str1, str2) =>
            //    {
            //        if (str1.Length > str1.Length)
            //        {
            //            return true;
            //        }

            //        if (str1.Length == str2.Length)
            //        {
            //            if (str1[0] < str2[0]) return false;

            //            for (int i = 1; i < str1.Length; i++)
            //            {
            //                if (str1[i] > str2[i])
            //                {
            //                    return true;
            //                }
            //            }
            //        }

            //        return false;
            //    });

            #endregion

            Console.WriteLine("Массив после сортировки:");

            foreach (var item in arr)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Two strings comparison.
        /// </summary>
        /// <param name="str1">first string</param>
        /// <param name="str2">second string</param>
        /// <returns>Returns true if the length of the first string
        /// is greater than the second. Or the letter at the index 
        /// of the first string is greater than the second; otherwise false.</returns>
        static bool Compare(string str1, string str2)
        {
            if (str1.Length > str2.Length)
            {
                return true;
            }

            if (str1.Length == str2.Length)
            {
                if (str1[0] < str2[0]) return false;

                for (int i = 1; i < str1.Length; i++)
                {
                    if (str1[i] > str2[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Sort an arbitrary array in ascending order.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <typeparam name="T">array type</typeparam>
        /// <param name="array">array to be sorted</param>
        /// <param name="compare">comparison delegate</param>
        static void Sort<T>(T[] array, Func<T, T, bool> compare)
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
    }
}
