using System;
using System.Collections.Generic;

namespace _31_Lost
{
    class Program
    {
        static void Main()
        {
            do
            {
                Console.WriteLine($"The winner is person with the number {GetWinner()}");

            } while (IsContinue());
        }

        /// <summary>
        /// Возвращает список людей в кругу.
        /// </summary>
        static List<int> GetCircleOfPeople()
        {
            int peopleCount = InputValue("Введите кол-во людей в кругу: ");

            List<int> tempList = new List<int>();

            for (int i = 0; i < peopleCount; i++)
            {
                tempList.Add(i + 1);
            }

            return tempList;
        }

        /// <summary>
        /// Возвращает список случайно пронумерованных людей в кругу.
        /// </summary>
        static List<int> GetRandomCircleOfPeople()
        {
            int randomNum;
            Random r = new Random();

            int peopleCount = InputValue("Введите кол-во людей в кругу: ");

            List<int> tempList = new List<int>();

            for (int i = 0; i < peopleCount; i++)
            {
                do
                {
                    randomNum = r.Next(1, peopleCount + 1);

                } while (tempList.Contains(randomNum));

                tempList.Add(randomNum);
            }

            return tempList;
        }

        /// <summary>
        /// Возвращает последнего человека в кругу.
        /// </summary>
        static int GetWinner()
        {
            int count; // кол-во строк в списке.
            List<int> people;

            if (IsRandom())
            {
                people = GetRandomCircleOfPeople();

            }
            else
            {
                people = GetCircleOfPeople();
            }


            do
            {
                count = people.Count;

                for (int i = 0; i < count; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        Console.WriteLine($"Вышел из круга человек под номером {people[i]}");
                        people[i] = 0;
                    }
                }

                people.RemoveAll(p => p == 0);

            } while (count != 1);

            return people[0];
        }

        /// <summary>
        /// Осуществляет выбор на повторение ввода.
        /// </summary>
        static bool IsContinue()
        {
            Console.WriteLine(Environment.NewLine
                              + "Начать заново? 1 - Да, 2 - Завершить программу.");

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
        /// Выбор будет ли список заполнятся случайно.
        /// </summary>
        static bool IsRandom()
        {
            Console.WriteLine("Как заполнить список нумерации людей?"
                              + " 1 - Случайно, 2 - Последовательно");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
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
        static int InputValue(string line)
        {
            while (true)
            {
                Console.Write(line);

                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                {
                    return value;
                }

                Console.WriteLine("Вводимое должно быть натуральное целое положительное "
                                  + "число и не равно 0.");
                Console.WriteLine();
            }
        }
    }
}
