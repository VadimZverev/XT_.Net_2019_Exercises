using System;

namespace _19_NonNegativeSum
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

            Console.Write("Массив: ");
            Show(array);

            Console.Write($"Сумма неотрицательных элементов в массиве: {NonNegativSum(array)}\n");
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
