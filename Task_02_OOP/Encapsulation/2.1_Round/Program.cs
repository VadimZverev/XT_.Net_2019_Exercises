using System;

namespace _21_Round
{
    class Program
    {
        static void Main()
        {
            Round round = new Round();

            do
            {
                round.X = InputValue("Введите координату X: ");
                round.Y = InputValue("Введите координату Y: ");

                while (true)
                {
                    try
                    {
                        round.Radius = InputValue("Введите радиус: ");
                    }
                    catch (ArgumentException exc)
                    {
                        Console.WriteLine(exc.Message);
                        continue;
                    }

                    break;
                }

                Console.WriteLine($"Координаты окружности: x = {round.X}, y = {round.Y}");
                Console.WriteLine($"Площадь окружности равна: {round.Area: #.###}");
                Console.WriteLine($"Длина окружности равна: {round.Circumference: #.###}");

                Console.WriteLine("Начать заново? 1 - Да, 2 - Завершить программу.");
            } while (IsContinue());
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
        /// Возвращает число с проверкой на корректность данных.
        /// </summary>
        /// <param name="value">вводимое значение данных.</param>
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
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    continue;
                }

                return value;
            }
        }
    }
}
