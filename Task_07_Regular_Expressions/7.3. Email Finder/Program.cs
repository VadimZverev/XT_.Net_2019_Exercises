using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace _73_Email_Finder
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

                Console.WriteLine();
                Console.WriteLine("Email Addresses Found:");

                foreach (var email in sentence.FindEmails())
                {
                    Console.WriteLine(email);
                }

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
        /// Return all mail matches in the text.
        /// </summary>
        public static IEnumerable FindEmails(this string @string)
        {
            string pattern = @"\b(([\dA-Za-z][\w-\.]*[\dA-Za-z])|([\dA-Za-z])+)@"
                             + @"(([\dA-Za-z][\dA-Za-z-]*[\dA-Za-z]\.)|([\dA-Za-z]\.))+"
                             + @"([A-Za-z]{2,6})\b";

            return new Regex(pattern).Matches(@string);
        }
    }
}
