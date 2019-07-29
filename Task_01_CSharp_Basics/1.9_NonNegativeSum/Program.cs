using System;

namespace _19_NonNegativeSum
{
    class Program
    {
        static void Main()
        {
            int[] array;
            int sizeArray, minValue, maxValue;

            do
            {
                Console.Write("Enter the number of elements in the array: ");
                sizeArray = InputSize();

                ChoiceOptions(out int value);

                switch (value)
                {
                    case 1:
                        Console.Write("Enter the minimum value of the element in the array: ");
                        InputValue(out minValue);

                        Console.Write("Enter the maximum value of the element in the array: ");
                        InputValue(out maxValue);

                        array = CreateArray(sizeArray, minValue, maxValue);
                        break;
                    case 2:
                        Console.Write("Enter the minimum value of the element in the array: ");
                        InputValue(out minValue);

                        array = CreateArray(sizeArray, minValue);
                        break;
                    case 3:
                        Console.Write("Enter the maximum value of the element in the array: ");
                        InputValue(out maxValue);

                        array = CreateArray(sizeArray, maxValue: maxValue);
                        break;
                    default:
                        array = CreateArray(sizeArray);
                        break;
                }

                Console.Write("Array: ");
                ShowArray(array);

                Console.WriteLine($"The sum of non-negative elements in the array: {NonNegativSum(array)}");

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
        /// Creates a one-dimensional array.
        /// </summary>
        /// <param name="sizeArray">array size</param>
        /// <param name="minValue">the minimum number of a range of values within each cell of the array.</param>
        /// <param name="maxValue">maximum number of value ranges within each cell of the array.</param>
        /// <returns>Returns a one-dimensional array object.</returns>
        static int[] CreateArray(int sizeArray, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int[] array = new int[sizeArray];
            Random random = new Random();

            for (int i = 0; i < sizeArray; i++)
            {
                array[i] = random.Next(minValue, maxValue);
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
        /// Calculates the sum of non-negative values in an array.
        /// </summary>
        /// <param name="array">array to calculate the sum</param>
        static int NonNegativSum(int[] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > 0)
                {
                    sum += array[i];
                }
            }

            return sum;
        }

        /// <summary>
        /// Display an array in the console.
        /// </summary>
        /// <param name="array">display array</param>
        static void ShowArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }
    }
}
