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
                    Console.WriteLine("Введите предложение на английском языке:");
                    Console.WriteLine();

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

                Console.WriteLine("Количество повторяющихся слов:");
                Console.WriteLine();

                foreach (var word in words)
                {
                    Console.WriteLine($"{word.Key}: {word.Value} раз.");
                }

            } while (IsContinue());
        }

        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
        static bool IsContinue()
        {
            Console.WriteLine(Environment.NewLine
                              + "Начать заново? 1 - Да, 2 - Завершить программу.");

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

        static bool IsManyally()
        {
            Console.WriteLine("Ввести вручную? 1 - Да, 2 - считать из файла.");

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
        /// Чтение из файла.
        /// </summary>
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
