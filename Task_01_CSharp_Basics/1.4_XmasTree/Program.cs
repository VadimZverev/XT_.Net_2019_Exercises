using System;

namespace _14_XmasTree
{
    class Program
    {
        static void Main()
        {
            do
            {
                int number = InputPositiveValue();
                XmasTree(number);

            } while (IsContinue());
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

        /// <summary>
        /// Draws a Xmas tree from triangles.
        /// </summary>
        /// <param name="value">the number of triangles in the Xmas tree</param>
        static void XmasTree(int value)
        {
            for (int k = 1; k <= value; k++)
            {
                for (int i = 1; i <= k; i++)
                {
                    for (int j = 1; j < value + i; j++)
                    {
                        if (j <= value - i)
                            Console.Write(' ');
                        else
                            Console.Write('*');
                    }

                    Console.WriteLine();
                } 
            }
        }
    }
}
