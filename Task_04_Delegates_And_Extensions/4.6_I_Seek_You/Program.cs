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
                posElements = array.Where(x => x > 0).ToArray();

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
        /// <param name="array">An array from which only positive elements 
        /// are extracted.</param>
        /// <returns>Returns true if the number is greater than zero, 
        /// otherwise false.</returns>
        public static int[] GetPositiveElements(int[] array)
        {
            List<int> temp = new List<int>();

            foreach (var item in array)
            {
                if (item > 0)
                {
                    temp.Add(item);
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// An array from which only elements that match the passed condition
        /// are extracted.
        /// </summary>
        /// <param name="array">An array from which only positive elements
        /// are extracted.</param>
        /// <param name="condition">The delegate, through which is passed a 
        /// condition extraction elements.</param>
        /// <returns>An array from which only elements that match the passed 
        /// condition are extracted.</returns>
        public static int[] GetElementsViaCondition(int[] array, Predicate<int> condition)
        {
            if (condition == null)
            {
                throw new ArgumentException("Empty condition delegate.");
            }

            List<int> temp = new List<int>();

            foreach (int item in array)
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
        /// <param name="array">array to extract elements</param>
        public static void ShowElements(int[] array)
        {
            foreach (int item in array)
            {
                Console.Write(item + ", ");
            }

            Console.WriteLine($"\b\b.{Environment.NewLine}");
        }
    }
}
