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

            } while (IsContinue());
        }


        /// <summary>
        /// Возвращает <c>true</c>, если выбрано повторение ввода, иначе <c>false</c>.
        /// </summary>
        static bool IsContinue()
        {
            Console.WriteLine("Начать заново? 1 - Да, 2 - Завершить программу.");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        return true;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        return false;
                }
            }
        }

        /// <summary>
        /// Возвращает число с проверкой на корректность данных.
        /// </summary>
        /// <param name="line">Информирующая строка.</param>
        /// <param name="isRadius"><c>true</c>, если вводимое число является радиусом.
        /// По умолчанию <c>false</c></param>
        static int InputValue(string line, bool isRadius = false)
        {
            while (true)
            {
                Console.Write(line);

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (!isRadius)
                    {
                        return value;
                    }
                    else
                    {
                        if (value > 0) return value;

                        Console.WriteLine("Радиус не может быть меньше, либо равен 0.");
                        continue;
                    }
                }

                Console.WriteLine("Вводимое должно быть натуральное целое число.");
            }
        }
    }
}
