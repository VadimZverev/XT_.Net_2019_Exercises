using System;

namespace _110_2DArray
{
    class Program
    {
        static void Main()
        {
            int[,] array2D;
            int rows, columns, minValue, maxValue, value;

            do
            {
                Console.WriteLine("Enter the number of elements in the two-dimensional array. ");
                InputSizeArray2D(out rows, out columns);

                ChoiceOptions(out value);

                switch (value)
                {
                    case 1:
                        Console.Write("Enter the minimum value of the element in the array: ");
                        InputValue(out minValue);

                        Console.Write("Enter the maximum value of the element in the array: ");
                        InputValue(out maxValue);

                        array2D = CreateArray2D(rows, columns, minValue, maxValue);
                        break;
                    case 2:
                        Console.Write("Enter the minimum value of the element in the array: ");
                        InputValue(out minValue);

                        array2D = CreateArray2D(rows, columns, minValue);
                        break;
                    case 3:
                        Console.Write("Enter the maximum value of the element in the array: ");
                        InputValue(out maxValue);

                        array2D = CreateArray2D(rows, columns, maxValue: maxValue);
                        break;
                    default:
                        array2D = CreateArray2D(rows, columns);
                        break;
                }

                Console.WriteLine("Array content:");
                ShowArray2D(array2D);

                Console.WriteLine($"{Environment.NewLine}The sum of the elements standing in even " +
                                        $"positions: {SumOfPositiveValues(array2D)}");

            } while (IsContinue());
        }

        /// <summary>
        /// Selection of data entry options for creating an array.
        /// </summary>
        /// <param name="value">input value</param>
        static void ChoiceOptions(out int value)
        {
            while (true)
            {
                Console.WriteLine($"Input parameters:{Environment.NewLine}"
                                  + $"\t1: Entering the minimum and maximum;{Environment.NewLine}"
                                  + $"\t2: Only minimum;{Environment.NewLine}"
                                  + $"\t3: Only maximum;{Environment.NewLine}"
                                  + $"\t4: Default;");
                Console.Write("Your choice: ");
                if (int.TryParse(Console.ReadLine(), out value)
                    && value > 0 && value <= 4)
                    break;
                else
                    Console.WriteLine("Choose from the list of values.");
            }
        }

        /// <summary>
        /// Creates a two-dimensional array.
        /// </summary>
        /// <param name="rows">how many rows in the array.</param>
        /// <param name="columns">how many columns will be in the array.</param>
        /// <param name="minValue">the minimum number of a range of values within each cell of the array.</param>
        /// <param name="maxValue">maximum number of value ranges within each cell of the array.</param>
        /// <returns>Returns a two-dimensional array object.</returns>
        static int[,] CreateArray2D(int rows, int columns, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int[,] array = new int[rows, columns];
            Random random = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array[i, j] = random.Next(minValue, maxValue);
                }
            }

            return array;
        }

        /// <summary>
        /// Enter size with a check for a natural number greater than 0.
        /// </summary>
        static int InputSize()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                    return value;
                else
                    Console.WriteLine("Input must be greater than 0 or a positive integer.");

                Console.Write("Enter: ");
            }
        }

        /// <summary>
        /// Data entry two-dimensional array.
        /// </summary>
        /// <param name="rows">number of rows.</param>
        /// <param name="columns">number of columns.</param>
        static void InputSizeArray2D(out int rows, out int columns)
        {
            Console.Write("Enter the number of rows: ");
            rows = InputSize();

            Console.Write("Enter the number of columns: ");
            columns = InputSize();
        }

        /// <summary>
        /// Entering numeric data with validation check for natural number.
        /// </summary>
        /// <param name="value">input data value</param>
        static void InputValue(out int value)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out value))
                    break;
                else
                    Console.WriteLine("The input must be a natural integer.");

                Console.Write("Enter: ");
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
        /// Display an array in the console.
        /// </summary>
        /// <param name="array">display array</param>
        static void ShowArray2D(int[,] array)
        {
            int rows = array.GetUpperBound(0) + 1;
            int columns = array.GetUpperBound(1) + 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{array[i, j],3} ");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Calculates the sum of the values at even positions 
        /// in the array.
        /// </summary>
        /// <param name="array">calculated array</param>
        /// <returns>Returns the sum of values.</returns>
        static int SumOfPositiveValues(int[,] array)
        {
            int sum = 0;
            int rows = array.GetUpperBound(0) + 1;
            int columns = array.GetUpperBound(1) + 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        sum += array[i, j];
                    }
                }
            }

            return sum;
        }
    }
}
