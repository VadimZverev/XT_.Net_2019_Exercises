using System;

namespace _02_Simple
{
    class Program
    {
        static void Main()
        {
            do
            {
                int number = InputValue();
                ShowSimpleOrComposite(number);

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
                Console.Write("Enter a number (from 2 and up): ");
                bool isParse = int.TryParse(Console.ReadLine(), out int number);

                if (isParse && number > 1)
                    return number;

                Console.WriteLine("Invalid input, you must enter an integer number greater than one.");
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
        /// Displays whether the number being checked is simple or composite.
        /// </summary>
        /// <param name="value">number to be tested</param>
        static void ShowSimpleOrComposite(int value)
        {
            for (int i = 2; i < value; i++)
            {
                if (value % i == 0)
                {
                    Console.WriteLine("Number is composite.");
                    return;
                }
            }

            Console.WriteLine("Number is simple.");
        }
    }
}
