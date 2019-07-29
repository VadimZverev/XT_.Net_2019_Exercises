using System;

namespace _111_AverageStringLength
{
    class Program
    {
        static void Main()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;

            do
            {
                Console.Write("Enter the string: ");
                string str = Console.ReadLine();

                int averageLength = AverageStringLength(str);
                Console.WriteLine($"The average words length is equal to {averageLength}");

            } while (IsContinue());
        }

        /// <summary>
        /// Calculates the average value of words in a sentence.
        /// </summary>
        /// <param name="str">check string</param>
        static int AverageStringLength(string str)
        {
            int sum = 0;
            char[] separators = { ' ', ',', '.', '!', '?', ':', ';', '"' };

            string[] words = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                sum += words[i].Length;
            }
            sum /= words.Length;

            return sum;
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
    }
}