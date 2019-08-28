using System;
using System.Text.RegularExpressions;

namespace _71_Date_Existence
{
    class Program
    {
        static void Main()
        {
            string pattern = @"((0[1-9])|([1-2][0-9])|([3][01]))-((0[1-9])|(1[0-2]))-\d{4}";

            do
            {
                Console.WriteLine("Input sentence with(out) date:");

                string sentence = Console.ReadLine();

                if (sentence.ContainsDate(pattern))
                {
                    Console.WriteLine($"The text \"{sentence}\" contains date.");
                }
                else
                    Console.WriteLine($"The text \"{sentence}\" doesn't contains date.");

                Console.WriteLine(Environment.NewLine
                                  + "Start over? 1 - Yes, 2 - Complete program.");
            } while (IsContinue());
        }

        /// <summary>
        /// Select to repeat input.
        /// </summary>
        static bool IsContinue()
        {
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
    }


    public static class MyExt
    {
        /// <summary>
        /// Checks if the date is in the text in a pattern.
        /// </summary>
        public static bool ContainsDate(this string @string, string pattern)
        {
            return new Regex(@"((0[1-9])|([1-2][0-9])|([3][01]))-((0[1-9])|(1[0-2]))-\d{4}").IsMatch(@string);
        }
    }
}
