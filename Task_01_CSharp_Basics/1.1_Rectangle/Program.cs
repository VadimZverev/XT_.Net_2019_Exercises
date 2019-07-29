using System;
using System.Text;

namespace _11_Rectangle
{
    class Program
    {
        static void Main()
        {
            do
            {
                InputValue('A', out int a);
                InputValue('B', out int b);

                Console.WriteLine($"The area of the rectangle is: {a * b}");

            } while (IsContinue());
        }

        /// <summary>
        /// Processes data entry, ignoring input punctuation and letters.
        /// </summary>
        /// <returns>Returns the string value entered from the console.</returns>
        static string GetIntFromConsole()
        {
            bool isFinished = false;

            StringBuilder sb = new StringBuilder();

            while (!isFinished)
            {
                ConsoleKeyInfo btnPress = Console.ReadKey(true);

                switch (btnPress.Key)
                {
                    case ConsoleKey.Enter:
                        isFinished = true;
                        break;
                    case ConsoleKey.Backspace:
                        if (sb.Length != 0)
                        {
                            Console.Write("\b \b");
                            sb.Remove(sb.Length - 1, 1);
                        }
                        break;
                    case ConsoleKey.OemMinus:
                        if (sb.Length == 0)
                        {
                            sb.Append(btnPress.KeyChar);
                            Console.Write(btnPress.KeyChar);
                        }
                        break;
                    default:
                        if (char.IsDigit(btnPress.KeyChar))
                        {
                            sb.Append(btnPress.KeyChar);
                            Console.Write(btnPress.KeyChar);
                        }
                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Enter the value of the side of the rectangle.
        /// </summary>
        /// <param name="sideName">Side name</param>
        /// <param name="side">Side size</param>
        static void InputValue(char sideName, out int side)
        {
            while (true)
            {
                Console.Write($"Enter side {sideName}: ");
                bool isParse = int.TryParse(GetIntFromConsole(), out side);

                if (isParse && side > 0)
                    break;
                else
                    Console.WriteLine($"{Environment.NewLine}Negative numbers and 0 are not allowed.");
            }
            Console.WriteLine();
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
