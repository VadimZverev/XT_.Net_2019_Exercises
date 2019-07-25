using System;
using System.Collections.Generic;
using System.IO;

namespace _32_Word_Frequency
{
    class Program
    {
        static void Main()
        {
            string temp;
            Dictionary<string, int> words;

            do
            {
                words = new Dictionary<string, int>();
                temp = string.Empty;

                if (IsManyally())
                {
                    Console.WriteLine("Enter sentence in English:" + Environment.NewLine);

                    temp = Console.ReadLine();
                }
                else
                    temp = ReadFromFile();

                char[] separators = { ',', '.', '!', '?', ';', ':', ' ' };
                string[] strings = temp.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                foreach (string str in strings)
                {
                    temp = str.ToLowerInvariant();

                    if (words.ContainsKey(temp))
                    {
                        words[temp]++;
                    }
                    else
                    {
                        words.Add(temp, 1);
                    }
                }

                Console.WriteLine("Number of duplicate words:");
                Console.WriteLine();

                foreach (var word in words)
                {
                    Console.WriteLine($"{word.Key}: {word.Value} times.");
                }

            } while (IsContinue());
        }

        /// <summary>
        /// Select to repeat input.
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
        /// Whether will manual input.
        /// </summary>
        /// <returns>Returns true if input will be done manually; otherwise false</returns>
        static bool IsManyally()
        {
            Console.WriteLine("Enter manually? 1 - Yes, 2 - read from file.");

            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        return true;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return false;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Read from file.
        /// </summary>
        /// <returns>Returns the string read from the file.</returns>
        static string ReadFromFile()
        {
            string temp;
            string path = Directory.GetCurrentDirectory() + "\\text.txt";

            using (StreamReader sr = new StreamReader(path))
            {
                temp = sr.ReadToEnd();
            }

            return temp;
        }
    }
}
