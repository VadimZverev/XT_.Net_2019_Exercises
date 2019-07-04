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
                Console.WriteLine("Введите число элементов в двумерном массиве. ");
                InputArray2DSize(out rows, out columns);

                ChoiceOptions(out value);

                switch (value)
                {
                    case 1:
                        Console.Write("Введите минимальное значение элемента в массиве: ");
                        InputValue(out minValue);

                        Console.Write("Введите максимальное значение элемента в массиве: ");
                        InputValue(out maxValue);

                        array2D = Array2DCreate(rows, columns, minValue, maxValue);
                        break;
                    case 2:
                        Console.Write("Введите минимальное значение элемента в массиве: ");
                        InputValue(out minValue);

                        array2D = Array2DCreate(rows, columns, minValue);
                        break;
                    case 3:
                        Console.Write("Введите максимальное значение элемента в массиве: ");
                        InputValue(out maxValue);

                        array2D = Array2DCreate(rows, columns, maxValue: maxValue);
                        break;
                    default:
                        array2D = Array2DCreate(rows, columns);
                        break;
                }

                Console.WriteLine("Содержимое массива:");
                Array2DShow(array2D);

                Console.WriteLine($"{Environment.NewLine}Сумма элементов, стоящих на чётных " +
                                        $"позициях: {SumOfPositiveValues(array2D)}");

                Console.WriteLine("Начать заново? 1 - Да, 2 - Выход из программы");
            } while (IsContinue());
        }

        /// <summary>
        /// Создаёт двумерный массив.
        /// </summary>
        /// <param name="rows">сколько строк будет в массиве.</param>
        /// <param name="columns">сколько столбцов будет в массиве.</param>
        /// <param name="minValue">минимальное число диапозона значений внутри каждой ячейки массива.</param>
        /// <param name="maxValue">максимальное число диапозона значений внутри каждой ячейки массива.</param>
        /// <returns>Возвращает объект двумерного массива.</returns>
        static int[,] Array2DCreate(int rows, int columns, int minValue = int.MinValue, int maxValue = int.MaxValue)
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
        /// Отображение массива в консоль.
        /// </summary>
        /// <param name="array">отображаемый массив.</param>
        static void Array2DShow(int[,] array)
        {
            int rows = array.GetUpperBound(0) + 1;
            int columns = array.GetUpperBound(1) + 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{array[i, j]} ");
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Выбор варианта ввода данных для создания массива.
        /// </summary>
        /// <param name="value">вводимое значение.</param>
        static void ChoiceOptions(out int value)
        {
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
        }

        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
        /// <returns>Возвращает bool-значение.</returns>
        static bool IsContinue()
        {
            while (true)
            {
                Console.Write("Ваш ввод: ");
                bool isParse = int.TryParse(Console.ReadLine(), out int value);

                if (isParse && value == 1)
                {
                    Console.Clear();
                    return true;
                }
                else if (isParse && value == 2) return false;
                else Console.WriteLine("Некорректный ввод, повторите ввод.");
            }
        }

        /// <summary>
        /// Ввод числовых данных с проверкой на корректность данных. Если вводимое число является размером массива, осуществляется проверка
        /// на положительное и натуральное число. Иначе только на натуральное число.
        /// </summary>
        /// <param name="value">вводимое значение данных.</param>
        /// <param name="isSizeArray">явяется ли вводимое значение размером массива. По умолчанию false.</param>
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

        /// <summary>
        /// Ввод данных двумерного массива.
        /// </summary>
        /// <param name="rows">число строк.</param>
        /// <param name="columns">число столбцов.</param>
        static void InputArray2DSize(out int rows, out int columns)
        {
            Console.Write("Введите число строк: ");
            InputValue(out rows, true);

            Console.Write("Введите число столбцов: ");
            InputValue(out columns, true);
        }

        /// <summary>
        /// Вычисляет сумму положительных значений, 
        /// стоящих на чётных позициях в массиве.
        /// </summary>
        /// <param name="array">вычисляемый массив.</param>
        /// <returns>возвращает сумму значений.</returns>
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
