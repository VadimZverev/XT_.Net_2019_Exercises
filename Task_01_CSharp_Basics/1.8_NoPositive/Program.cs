using System;

namespace _18_NoPositive
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,,] array3D;
            int demension, rows, columns, minValue, maxValue, value;

            do
            {
                Console.WriteLine("Введите число элементов в 3-х мерном массиве. ");
                InputArray3DSize(out demension, out rows, out columns);

                ChoiceOptions(out value);

                switch (value)
                {
                    case 1:
                        Console.Write("Введите минимальное значение элемента в массиве: ");
                        InputValue(out minValue);

                        Console.Write("Введите максимальное значение элемента в массиве: ");
                        InputValue(out maxValue);

                        array3D = Array3DCreate(demension, rows, columns, minValue, maxValue);
                        break;
                    case 2:
                        Console.Write("Введите минимальное значение элемента в массиве: ");
                        InputValue(out minValue);

                        array3D = Array3DCreate(demension, rows, columns, minValue);
                        break;
                    case 3:
                        Console.Write("Введите максимальное значение элемента в массиве: ");
                        InputValue(out maxValue);

                        array3D = Array3DCreate(demension, rows, columns, maxValue: maxValue);
                        break;
                    default:
                        array3D = Array3DCreate(demension, rows, columns);
                        break;
                }

                Console.WriteLine($"{Environment.NewLine}Массив до замены:");
                Array3DShow(array3D);

                Console.WriteLine("Массив после замены:");

                SetZeroForPositiveValues(array3D);
                Array3DShow(array3D);

                Console.WriteLine("Начать заново? 1 - Да, 2 - Выход из программы");
            } while (IsContinue());
        }

        /// <summary>
        /// Создаёт трёхмерный массив.
        /// </summary>
        /// <param name="demension">сколько измерений будет в массиве.</param>
        /// <param name="rows">сколько строк будет в массиве.</param>
        /// <param name="columns">сколько столбцов будет в массиве.</param>
        /// <param name="minValue">минимальное число диапозона значений внутри каждой ячейки массива.</param>
        /// <param name="maxValue">максимальное число диапозона значений внутри каждой ячейки массива.</param>
        /// <returns>Возвращает объект трёхмерного массива.</returns>
        static int[,,] Array3DCreate(int demension, int rows, int columns, int minValue = int.MinValue, int maxValue = int.MaxValue)
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

        /// <summary>
        /// Отображение массива в консоль.
        /// </summary>
        /// <param name="array">отображаемый массив.</param>
        static void Array3DShow(int[,,] array)
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
        /// Ввод данных трёхмерного массива.
        /// </summary>
        /// <param name="demension">число измерений.</param>
        /// <param name="rows">число строк.</param>
        /// <param name="columns">число столбцов.</param>
        static void InputArray3DSize(out int demension, out int rows, out int columns)
        {
            Console.Write("Введите число измерений: ");
            InputValue(out demension, true);

            Console.Write("Введите число строк: ");
            InputValue(out rows, true);

            Console.Write("Введите число столбцов: ");
            InputValue(out columns, true);
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
        /// Устанавливает все положительные значения в трёхмерном массиве в 0.
        /// </summary>
        /// <param name="array">изменяемый массив.</param>
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
    }
}
