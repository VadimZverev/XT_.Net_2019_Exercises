using System;
using System.Collections.Generic;
using System.Linq;

namespace _46_I_Seek_You
{
    class Program
    {
        static void Main()
        {
            do
            {
                int[] posElements;
                int[] array = CreateRandomArray();

                Console.Write($"Array of numbers: ");
                ShowElements(array);

                posElements = GetPositiveElements(array);

                if (posElements.Length == 0)
                {
                    Console.WriteLine("No positive number(s).");
                    continue;
                }

                Console.WriteLine("The method that directly implements the search:");
                Console.Write($"Positive number(s) is: ");
                ShowElements(posElements);

                Console.WriteLine("The method to which the search condition is passed "
                                  + "through the delegate instance:");
                Predicate<int> condition = new Predicate<int>(IsPositiveNumber);
                posElements = GetElementsViaCondition(array, condition);

                Console.Write($"Positive number(s) is: ");
                ShowElements(posElements);

                Console.WriteLine("The method to which the search condition is passed "
                                  + "through the delegate as an anonymous method:");

                GetElementsViaCondition(array, delegate (int item)
                {
                    return item > 0;
                });

                Console.Write($"Positive number(s) is: ");
                ShowElements(posElements);

                Console.WriteLine("The method to which the search condition is passed "
                                  + "through the delegate as a lambda expression:");
                posElements = GetElementsViaCondition(array, item => item > 0);

                Console.Write($"Positive number(s) is: ");
                ShowElements(posElements);

                Console.WriteLine("LINQ expression:");
                posElements = (from item in array
                               where item > 0
                               select item).ToArray();

                Console.Write($"Positive number(s) is: ");
                ShowElements(posElements);

            } while (IsContinue());
        }

        public static int[] CreateRandomArray()
        {
            Random r = new Random();
            int[] temp = new int[10];

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = r.Next(-100, 100);
            }

            return temp;
        }

        /// <summary>
        /// Returns an array of positive integer elements.
        /// </summary>
        /// <typeparam name="T">type array</typeparam>
        /// <param name="array">An array from which only positive elements 
        /// are extracted.</param>
        /// <returns>Returns true if the number is greater than zero, 
        /// otherwise false.</returns>
        public static T[] GetPositiveElements<T>(T[] array)
        {
            bool isPositive = true;
            List<T> temp = new List<T>();

            foreach (var item in array)
            {
                switch (item)
                {
                    case double d:
                        isPositive = d > 0.0d;
                        break;
                    case float f:
                        isPositive = f > 0.0f;
                        break;
                    case decimal m:
                        isPositive = m > 0.0m;
                        break;
                }

                if (!isPositive) continue;

                string tempStr = item.ToString();

                if (tempStr.StartsWith('-') || tempStr == "0")
                {
                    continue;
                }

                temp.Add(item);
            }

            return temp.ToArray();
        }

        /// <summary>
        /// An array from which only elements that match the passed condition
        /// are extracted.
        /// </summary>
        /// <typeparam name="T">type array</typeparam>
        /// <param name="array">An array from which only positive elements
        /// are extracted.</param>
        /// <param name="condition">The delegate, through which is passed a 
        /// condition extraction elements.</param>
        /// <returns>An array from which only elements that match the passed 
        /// condition are extracted.</returns>
        public static T[] GetElementsViaCondition<T>(T[] array, Predicate<T> condition)
        {
            if (condition == null)
            {
                throw new ArgumentException("Empty condition delegate.");
            }

            List<T> temp = new List<T>();

            foreach (T item in array)
            {
                if (condition.Invoke(item))
                {
                    temp.Add(item);
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// Select to repeat demonstration.
        /// </summary>
        public static bool IsContinue()
        {
            Console.WriteLine(Environment.NewLine
                              + "Start over? 1 - Yes, 2 - Complete program.");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        return true;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return false;
                }
            }
        }
        /// <summary>
        /// Checks if a passed item is positive.
        /// </summary>
        /// <param name="item">transferred item</param>
        /// <returns>Returns true if the number is greater than zero,
        /// otherwise false.</returns>
        public static bool IsPositiveNumber(int item) => item > 0;

        /// <summary>
        /// Shows array elements.
        /// </summary>
        /// <typeparam name="T">type array</typeparam>
        /// <param name="array">array to extract elements</param>
        public static void ShowElements<T>(T[] array)
        {
            foreach (T item in array)
            {
                Console.Write(item + ", ");
            }

            Console.WriteLine($"\b\b.{Environment.NewLine}");
        }
    }
}
