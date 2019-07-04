using System;

namespace _17_ArrayProcessing
{
    class Program
    {
        static void Main()
        {
            int[] array;
            int sizeArray, minValue, maxValue, value;

            do
            {
                Console.Write("Введите число элементов в массиве: ");
                InputArraySizeValue(out sizeArray, isSizeArray: true);

                ChoiceOptions(out value);

                switch (value)
                {
                    case 1:
                        Console.Write("Введите минимальное значение элемента в массиве: ");
                        InputArraySizeValue(out minValue);

                        Console.Write("Введите максимальное значение элемента в массиве: ");
                        InputArraySizeValue(out maxValue);

                        array = ArrayCreate(sizeArray, minValue, maxValue);
                        break;
                    case 2:
                        Console.Write("Введите минимальное значение элемента в массиве: ");
                        InputArraySizeValue(out minValue);

                        array = ArrayCreate(sizeArray, minValue);
                        break;
                    case 3:
                        Console.Write("Введите максимальное значение элемента в массиве: ");
                        InputArraySizeValue(out maxValue);

                        array = ArrayCreate(sizeArray, maxValue: maxValue);
                        break;
                    default:
                        array = ArrayCreate(sizeArray);
                        break;
                }

                Console.WriteLine($"{Environment.NewLine}Массив до сортировки:");
                ArrayShow(array);

                Console.WriteLine();

                Console.WriteLine($"Минимальное значение в массиве: {MinArrayValue(array)}");
                Console.WriteLine($"Максимальное значение в массиве: {MaxArrayValue(array)}" +
                                    Environment.NewLine);

                Console.WriteLine("Массив после сортировки:");
                ArraySort(array);
                ArrayShow(array);

                Console.WriteLine("Начать заново? 1 - Да, 2 - Выход из программы");
            } while (IsContinue());
        }

        /// <summary>
        /// Создаёт одномерный массив.
        /// </summary>
        /// <param name="sizeArray">размер массива.</param>
        /// <param name="minValue">минимальное число диапозона значений внутри каждой ячейки массива.</param>
        /// <param name="maxValue">максимальное число диапозона значений внутри каждой ячейки массива.</param>
        /// <returns>Возвращает объект одномерного массива.</returns>
        static int[] ArrayCreate(int sizeArray, int minValue = int.MinValue, int maxValue = int.MaxValue)
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
        /// Отображение массива в консоль.
        /// </summary>
        /// <param name="array">отображаемый массив.</param>
        static void ArrayShow(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Сортировка массива.
        /// </summary>
        /// <param name="array">сортируемый массив.</param>
        static void ArraySort(int[] array)
        {
            int[] arr = array;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array.Length > j + 1)
                    {
                        if (arr[j] > arr[j + 1])
                        {
                            int temp = arr[j];
                            arr[j] = arr[j + 1];
                            arr[j + 1] = temp;
                        }
                    }
                }
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
        /// Ввод данных одномерного массива.
        /// </summary>
        /// <param name="size">размер массива.</param>
        /// <param name="isSizeArray">явяется ли вводимое значение размером массива. По умолчанию false.</param>
        static void InputArraySizeValue(out int size, bool isSizeArray = false)
        {
            while (true)
            {
                string str = Console.ReadLine();

                if (isSizeArray)
                {
                    if (int.TryParse(str, out size) && size > 0)
                        break;
                    else
                        Console.WriteLine("Ввод должен быть больше 0 либо натуральное положительное целое число.");
                }
                else
                {
                    if (int.TryParse(str, out size))
                        break;
                    else
                        Console.WriteLine("Вводимое должено быть натуральное целое число.");
                }

                Console.Write("Введите: ");
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
        /// Вычисляет максимальное значение массива.
        /// </summary>
        /// <param name="array">проверяемый массив.</param>
        /// <returns>Возвращает результирующее значение.</returns>
        static int MaxArrayValue(int[] array)
        {
            int maxValue = 0;
            bool isMax = true;

            for (int i = 0; i < array.Length; i++)
            {
                isMax = true;

                for (int j = 0; j < array.Length; j++)
                {
                    if (array.Length < j + 1) break;
                    if (array[i] == array[j]) continue;
                    if (array[i] < array[j])
                    {
                        isMax = false;
                        break;
                    }
                }

                if (isMax)
                {
                    maxValue = array[i];
                    break;
                }
                else
                {
                    maxValue = array[i];
                }
            }

            return maxValue;
        }

        /// <summary>
        /// Вычисляет минимальное значение массива.
        /// </summary>
        /// <param name="array">проверяемый массив.</param>
        /// <returns>Возвращает результирующее значение.</returns>
        static int MinArrayValue(int[] array)
        {
            int minValue = 0;
            bool isMin = true;

            for (int i = 0; i < array.Length; i++)
            {
                isMin = true;

                for (int j = 0; j < array.Length; j++)
                {
                    if (array.Length < j + 1) break;
                    if (array[i] == array[j]) continue;
                    if (array[i] > array[j])
                    {
                        isMin = false;
                        break;
                    }
                }

                if (isMin)
                {
                    minValue = array[i];
                    break;
                }
                else
                {
                    minValue = array[i];
                }
            }

            return minValue;
        }
    }
}
