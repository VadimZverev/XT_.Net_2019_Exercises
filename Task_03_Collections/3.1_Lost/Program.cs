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
        /// Returns a list of people in the circle.
        /// </summary>
        static List<int> GetCircleOfPeople()
        {
            int peopleCount = InputValue("Enter the number of people in the circle: ");

            List<int> tempList = new List<int>();

            for (int i = 0; i < peopleCount; i++)
            {
                tempList.Add(i + 1);
            }

            return tempList;
        }

        /// <summary>
        /// Returns a list of randomly numbered people in a circle..
        /// </summary>
        static List<int> GetRandomCircleOfPeople()
        {
            int randomNum;
            Random r = new Random();

            int peopleCount = InputValue("Enter the number of people in the circle: ");

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
        /// Returns the last person in the circle.
        /// </summary>
        static int GetWinner()
        {
            int count; // number of people
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
                        Console.WriteLine($"Out of the circle of people under the number {people[i]}");
                        people[i] = 0;
                    }
                }

                people.RemoveAll(p => p == 0);

            } while (count != 1);

            return people[0];
        }

        /// <summary>
        /// Select to repeat input.
        /// </summary>
        static bool IsContinue()
        {
            Console.WriteLine(Environment.NewLine
                              + "Start over? 1 - Yes, 2 - Complete program.");

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
        /// Choose whether the list will be filled randomly.
        /// </summary>
        static bool IsRandom()
        {
            Console.WriteLine("How to fill the list of people numbering?"
                              + " 1 - Random, 2 - Consistently");

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
        /// Returns a number with data validation.
        /// </summary>
        /// <param name="line">Informative line</param>
        static int InputValue(string line)
        {
            while (true)
            {
                Console.Write(line);

                if (int.TryParse(Console.ReadLine(), out int value) && value > 0)
                {
                    return value;
                }

                Console.WriteLine("The input must be a positive integer and not zero."
                                    + Environment.NewLine);
            }
        }
    }
}
