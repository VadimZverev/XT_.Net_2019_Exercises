using System;

namespace _110_2DArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array2D;
            int rows, columns, minValue, maxValue, value;

            Console.WriteLine("Введите число элементов в 2-х мерном массиве. ");
            InputSize2DArray(out rows, out columns);

            while (true)
            {
                Console.WriteLine("Параметры ввода:\n\t1: Ввод минимума и максимума;" +
                    "\n\t2: Только минимум;\n\t3: Только максимум;\n\t4: По умолчанию;");
                Console.Write("Ваш выбор: ");
                if (int.TryParse(Console.ReadLine(), out value)
                    && value > 0 && value <= 4)
                    break;
                else
                    Console.WriteLine("Выберете из списка значений");
            }

            if (value == 1)
            {
                Console.Write("Введите минимальное значение элемента в массиве: ");
                InputValue(out minValue);

                Console.Write("Введите максимальное значение элемента в массиве: ");
                InputValue(out maxValue);

                array2D = Create2DArray(rows, columns, minValue, maxValue);
            }
            else if (value == 2)
            {
                Console.Write("Введите минимальное значение элемента в массиве: ");
                InputValue(out minValue);

                array2D = Create2DArray(rows, columns, minValue);
            }
            else if (value == 3)
            {
                Console.Write("Введите максимальное значение элемента в массиве: ");
                InputValue(out maxValue);

                array2D = Create2DArray(rows, columns, maxValue: maxValue);
            }
            else
            {
                array2D = Create2DArray(rows, columns);
            }

            Console.WriteLine("Содержимое массива:");
            ShowArray(array2D);

            Console.WriteLine($"\nСумма элементов, стоящих на чётных позициях: {SumPositiveValue(array2D)}");
        }

        static int[,] Create2DArray(int rows, int columns, int minValue = int.MinValue, int maxValue = int.MaxValue)
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

        static void InputValue(out int value, bool isSizeArray = false)
        {
            while (true)
            {
                string str = Console.ReadLine();

                if (isSizeArray)
                {
                    if (int.TryParse(str, out value) && value > 0)
                        break;
                    else
                        Console.WriteLine("Ввод должен быть больше 0 либо натуральное положительное целое число.");
                }
                else
                {
                    if (int.TryParse(str, out value))
                        break;
                    else
                        Console.WriteLine("Вводимое должно быть натуральное целое число.");
                }

                Console.Write("Введите: ");
            }

        }

        static void InputSize2DArray(out int rows, out int columns)
        {
            Console.Write("Введите число строк: ");
            InputValue(out rows, true);

            Console.Write("Введите число столбцов: ");
            InputValue(out columns, true);
        }

        static void ShowArray(int[,] array)
        {
            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
        }

        static int SumPositiveValue(int[,] array)
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
