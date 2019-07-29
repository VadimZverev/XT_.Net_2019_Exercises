using System;

namespace _03_Square
{
    class Program
    {
        static void Main()
        {
            do
            {
                int number = InputValue();
                ShowSquare(number);

            } while (IsContinue());
        }

        /// <summary>
        /// Enter integer value.
        /// </summary>
        /// <returns>Returns an integer value.</returns>
        public static int InputValue()
        {
            while (true)
            {
                Console.Write("Enter a positive odd number and also more than 2: ");
                bool isParse = int.TryParse(Console.ReadLine(), out int number);

                if (isParse && number > 2 && (number % 2 != 0))
                    return number;

                Console.WriteLine("Invalid input, you must enter an integer number greater than two and not even.");
            }
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
        /// Shows a square of "*" with a space in the center.
        /// </summary>
        /// <param name="size">square size</param>
        static void ShowSquare(int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if ((size / 2 == i) && (size / 2 == j))
                        Console.Write(' ');
                    else
                        Console.Write('*');
                }
                Console.WriteLine();
            }
        }
    }
}
