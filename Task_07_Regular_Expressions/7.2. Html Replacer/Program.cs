using System;
using System.Text;
using System.Text.RegularExpressions;

namespace _72_Html_Replacer
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = Encoding.Unicode;

            do
            {
                Console.WriteLine("Input sentence:");

                string sentence = Console.ReadLine();

                sentence = sentence.ReplaceTag("_");

                Console.WriteLine($"Replace tags in text:");
                Console.WriteLine(sentence);

                Console.WriteLine();

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
        /// It replaces the tags on the selected character.
        /// </summary>
        public static string ReplaceTag(this string @string, string symbol)
        {
            return new Regex("<.+?>").Replace(@string, symbol);
        }
    }
}
