using System;

namespace _18_NoPositive
{
    class Program
    {
        static void Main()
        {
            int[,,] array3D;
            int dimension, rows, columns, minValue, maxValue, value;

            do
            {
                Console.WriteLine("Enter the number of elements in a 3-dimensional array.");
                InputArray3DSize(out dimension, out rows, out columns);

                ChoiceOptions(out value);

                switch (value)
                {
                    case 1:
                        Console.Write("Enter the minimum value of the element in the array: ");
                        InputValue(out minValue);

                        Console.Write("Enter the maximum value of the element in the array: ");
                        InputValue(out maxValue);

                        array3D = CreateArray3D(dimension, rows, columns, minValue, maxValue);
                        break;
                    case 2:
                        Console.Write("Enter the minimum value of the element in the array: ");
                        InputValue(out minValue);

                        array3D = CreateArray3D(dimension, rows, columns, minValue);
                        break;
                    case 3:
                        Console.Write("Enter the maximum value of the element in the array: ");
                        InputValue(out maxValue);

                        array3D = CreateArray3D(dimension, rows, columns, maxValue: maxValue);
                        break;
                    default:
                        array3D = CreateArray3D(dimension, rows, columns);
                        break;
                }

                Console.WriteLine($"{Environment.NewLine}Array before replacement:");
                ShowArray3D(array3D);

                Console.WriteLine("Array after replacement:");

                SetZeroForPositiveValues(array3D);
                ShowArray3D(array3D);

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
        /// Creates a three-dimensional array.
        /// </summary>
        /// <param name="dimension">how many dimensions will be in the array.</param>
        /// <param name="rows">how many rows will be in the array.</param>
        /// <param name="columns">how many columns will be in the array.</param>
        /// <param name="minValue">the minimum number of a range of values within each cell of the array.</param>
        /// <param name="maxValue">the maximum number of a range of values within each cell of the array.</param>
        /// <returns>Returns a three-dimensional array object.</returns>
        static int[,,] CreateArray3D(int dimension, int rows, int columns, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int[,,] array = new int[dimension, rows, columns];
            Random random = new Random();

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    for (int k = 0; k < columns; k++)
                    {
                        array[i, j, k] = random.Next(minValue, maxValue);
                    }
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
        /// Data entry three-dimensional array.
        /// </summary>
        /// <param name="dimension">number of dimension</param>
        /// <param name="rows">number of rows</param>
        /// <param name="columns">number of columns</param>
        static void InputArray3DSize(out int dimension, out int rows, out int columns)
        {
            Console.Write("Enter the number of dimension: ");
            dimension = InputSize();

            Console.Write("Enter the number of rows: ");
            rows = InputSize();

            Console.Write("Enter the number of columns: ");
            columns = InputSize();
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
        /// Sets all positive values in a three-dimensional array to zero.
        /// </summary>
        /// <param name="array">mutable array</param>
        static void SetZeroForPositiveValues(int[,,] array)
        {
            int dimension = array.GetUpperBound(0) + 1;
            int rows = array.GetUpperBound(1) + 1;
            int columns = array.GetUpperBound(2) + 1;

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    for (int k = 0; k < columns; k++)
                    {
                        if (array[i, j, k] > 0)
                        {
                            array[i, j, k] = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Display an array in the console.
        /// </summary>
        /// <param name="array">display array</param>
        static void ShowArray3D(int[,,] array)
        {
            int dimension = array.GetUpperBound(0) + 1;
            int rows = array.GetUpperBound(1) + 1;
            int columns = array.GetUpperBound(2) + 1;

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    for (int k = 0; k < columns; k++)
                    {
                        Console.Write($"{array[i, j, k],3} ");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    }
}
