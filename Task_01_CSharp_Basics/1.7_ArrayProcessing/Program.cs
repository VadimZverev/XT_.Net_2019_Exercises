using System;

namespace _17_ArrayProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array;
            int sizeArray, minValue, maxValue, value;

            Console.Write("Введите число элементов в массиве: ");
            InputValue(out sizeArray, true);

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

                array = CreateArray(sizeArray, minValue, maxValue);
            }
            else if (value == 2)
            {
                Console.Write("Введите минимальное значение элемента в массиве: ");
                InputValue(out minValue);

                array = CreateArray(sizeArray, minValue);
            }
            else if (value == 3)
            {
                Console.Write("Введите максимальное значение элемента в массиве: ");
                InputValue(out maxValue);

                array = CreateArray(sizeArray, maxValue: maxValue);
            }
            else
            {
                array = CreateArray(sizeArray);
            }

            Console.WriteLine("Массив до сортировки:");
            Show(array);

            Console.WriteLine();

            Console.WriteLine($"Минимальное значение в массиве: {MinValueArray(array)}");
            Console.WriteLine($"Максимальное значение в массиве: {MaxValueArray(array)}");

            Console.WriteLine("Массив после сортировки:");
            ArraySort(array);
            Show(array);

        }

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

        static int MinValueArray(int[] array)
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

        static int MaxValueArray(int[] array)
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

        static void Show(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }
    }
}
