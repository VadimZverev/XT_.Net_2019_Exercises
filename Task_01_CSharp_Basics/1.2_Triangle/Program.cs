using System;

namespace _12_Triangle
{
    class Program
    {
        static void Main()
        {
            do
            {
                int height = InputPositiveValue();
                DrawHalfTriangle(height);

            } while (IsContinue());
        }

        /// <summary>
        /// Draws a triangle.
        /// </summary>
        /// <param name="height">height</param>
        static void DrawHalfTriangle(int height)
        {
            for (int i = 1; i <= height; i++)
            {
                for (int j = 1; j <= i; j++)
                    Console.Write('*');

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Returns a positive integer.
        /// </summary>
        static int InputPositiveValue()
        {
            while (true)
            {
                Console.Write("Enter a positive number: ");
                bool isParse = int.TryParse(Console.ReadLine(), out int number);

                if (isParse && number > 0) return number;

                Console.WriteLine("Invalid input (characters, negative, 0 are not allowed)");
            }
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
