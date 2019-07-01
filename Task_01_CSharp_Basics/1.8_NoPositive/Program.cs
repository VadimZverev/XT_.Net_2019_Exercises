using System;

namespace _18_NoPositive
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,,] array3D;
            int demension, rows, columns, minValue, maxValue, value;

            Console.WriteLine("Введите число элементов в 3-х мерном массиве. ");
            InputSize3DArray(out demension, out rows, out columns);

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

                array3D = Create3DArray(demension, rows, columns, minValue, maxValue);
            }
            else if (value == 2)
            {
                Console.Write("Введите минимальное значение элемента в массиве: ");
                InputValue(out minValue);

                array3D = Create3DArray(demension, rows, columns, minValue);
            }
            else if (value == 3)
            {
                Console.Write("Введите максимальное значение элемента в массиве: ");
                InputValue(out maxValue);

                array3D = Create3DArray(demension, rows, columns, maxValue: maxValue);
            }
            else
            {
                array3D = Create3DArray(demension, rows, columns);
            }

            Console.WriteLine("Массив до замены:");
            ShowArray(array3D);

            Console.WriteLine("\nМассив после замены:");

            SetZeroPositiveValue(array3D);
            ShowArray(array3D);
        }

        static int[,,] Create3DArray(int demension, int rows, int columns, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int[,,] array = new int[demension, rows, columns];
            Random random = new Random();

            for (int i = 0; i < demension; i++)
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

        static void InputSize3DArray(out int demension, out int rows, out int columns)
        {
            Console.Write("Введите число измерений: ");
            InputValue(out demension, true);

            Console.Write("Введите число строк: ");
            InputValue(out rows, true);

            Console.Write("Введите число столбцов: ");
            InputValue(out columns, true);
        }

        static void SetZeroPositiveValue(int[,,] array)
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

        static void ShowArray(int[,,] array)
        {
            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
        }
    }
}
