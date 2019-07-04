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
                Console.Write("Введите число элементов в массиве: ");
                InputValue(out sizeArray, true);

                ChoiceOptions(out int value);

                switch (value)
                {
                    case 1:
                        Console.Write("Введите минимальное значение элемента в массиве: ");
                        InputValue(out minValue);

                        Console.Write("Введите максимальное значение элемента в массиве: ");
                        InputValue(out maxValue);

                        array = ArrayCreate(sizeArray, minValue, maxValue);
                        break;
                    case 2:
                        Console.Write("Введите минимальное значение элемента в массиве: ");
                        InputValue(out minValue);

                        array = ArrayCreate(sizeArray, minValue);
                        break;
                    case 3:
                        Console.Write("Введите максимальное значение элемента в массиве: ");
                        InputValue(out maxValue);

                        array = ArrayCreate(sizeArray, maxValue: maxValue);
                        break;
                    default:
                        array = ArrayCreate(sizeArray);
                        break;
                }

                Console.Write("Массив: ");
                ArrayShow(array);

                Console.WriteLine($"Сумма неотрицательных элементов в массиве: {NonNegativSum(array)}");

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
        /// <param name="size">вводимое значение данных.</param>
        /// <param name="isSizeArray">явяется ли вводимое значение размером массива. По умолчанию false.</param>
        static void InputValue(out int size, bool isSizeArray = false)
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
        /// Вычисляет сумму не отрицальных значений в массиве.
        /// </summary>
        /// <param name="array">массив для вычисления суммы.</param>
        /// <returns></returns>
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
    }
}
