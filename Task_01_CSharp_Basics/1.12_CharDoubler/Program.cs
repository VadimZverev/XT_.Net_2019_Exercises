using System;

namespace _112_CharDoubler
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;

            do
            {
                Console.Write("Enter the first line: ");
                string firstStr = Console.ReadLine();

                Console.Write("Enter the second line: ");
                string secondStr = Console.ReadLine();

                string result = ResultString(firstStr, secondStr);
                Console.Write($"Result string: {result}");

            } while (IsContinue());
        }

        /// <summary>
        /// Select to repeat demonstration.
        /// </summary>
        static bool IsContinue()
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
        /// Removes duplicate characters.
        /// </summary>
        /// <param name="changeString">editable string</param>
        /// <returns>Returns a string.</returns>
        static string RemoveDuplicateChars(string changeString)
        {
            string result = "";
            foreach (char ch in changeString)
                if (result.IndexOf(ch) == -1)
                    result += ch;
            return result;
        }

        /// <summary>
        /// Duplicates characters present in the string to be checked.
        /// </summary>
        /// <param name="changeString">changeable string</param>
        /// <param name="checkString">checking line</param>
        static string ResultString(string changeString, string checkString)
        {
            char[] checkChars = RemoveDuplicateChars(checkString).ToCharArray();

            foreach (char ch in checkChars)
            {
                if (changeString.Contains(ch))
                    changeString = changeString.Replace($"{ch}", $"{ch}{ch}");
            }

            return changeString;
        }
    }
}
