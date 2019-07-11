using System;

namespace _26_Ring
{
    class Program
    {
        static void Main()
        {
            Ring ring;
            do
            {
                int x = InputValue("Введите координату X: ");
                int y = InputValue("Введите координату Y: ");

                while (true)
                {

                    int inR = InputValue("Введите внутренний радиус: ");
                    int outR = InputValue("Введите внешний радиус: ");

                    try
                    {
                        ring = new Ring(inR, outR, x, y);
                    }
                    catch (ArgumentException exc)
                    {
                        Console.WriteLine(exc.Message);
                        continue;
                    }

                    break;
                }

                Console.WriteLine($"Координаты окружности: x = {ring.X}, y = {ring.Y}");
                Console.WriteLine($"Площадь кольца равна: {ring.Area: #.###}");
                Console.WriteLine($"Суммарная длина внешней и внутренней окружности равна: {ring.Circumference: #.###}");

                Console.WriteLine("Начать заново? 1 - Да, 2 - Завершить программу.");
            } while (IsContinue());
        }

        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
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
        /// Возвращает число с проверкой на корректность данных.
        /// </summary>
        /// <param name="line">Информирующая строка.</param>
        static int InputValue(string line)
        {
            int value;

            while (true)
            {
                Console.Write(line);

                try
                {
                    value = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Вводимое должно быть натуральное целое число.");
                    continue;
                }

                return value;
            }
        }
    }
}

