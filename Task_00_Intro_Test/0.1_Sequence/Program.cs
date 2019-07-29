using System;

namespace _01_Sequence
{
    class Program
    {
        static void Main()
        {
            do
            {
                int number = InputValue();
                ShowSequenceOfNumbers(number);

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
                Console.Write("Input number: ");
                bool isParse = int.TryParse(Console.ReadLine(), out int number);

                if (isParse && number > 0)
                    return number;

                Console.WriteLine("Invalid input, you must enter an integer number greater than zero.");
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
        /// Displays a sequence of numbers from 1 to the specified value.
        /// </summary>
        /// <param name="value">a number indicating how many numbers will
        /// be displayed.</param>
        static void ShowSequenceOfNumbers(int value)
        {
            Console.Write("Sequence of numbers: ");
            for (int i = 1; i <= value; i++)
            {
                Console.Write(i + ", ");
            }

            Console.WriteLine("\b\b.");
        }
    }
}
