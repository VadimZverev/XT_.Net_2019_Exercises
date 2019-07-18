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

                    int inR = InputValue("Введите внутренний радиус: ", true);
                    int outR = InputValue("Введите внешний радиус: ", true);

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
                Console.WriteLine($"Площадь кольца равна: {ring.Area:#.###}");
                Console.WriteLine($"Суммарная длина внешней и внутренней окружности равна: {ring.Circumference:#.###}");

            } while (IsContinue());
        }

        /// <summary>
        /// Осуществляет выбор на повторение ввода.
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

