using _61_Users.BLL;
using _61_Users.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace _61_Users.PL
{
    class Program
    {
        static void Main()
        {
            char select;

            do
            {
                select = SelectOption();

            } while (select != 'q' && select != 'й');

            UsersManager.Save();
        }


        private static char SelectOption()
        {
            Console.WriteLine("Please select some action:");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Remove Users");
            Console.WriteLine("3. Show Users");
            Console.WriteLine("Q. Exit");

            while (true)
            {
                var input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        AddUser();
                        return input.KeyChar;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        RemoveUser();
                        return input.KeyChar;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ShowUsers(UsersManager.GetAllUsers());
                        return input.KeyChar;
                    case ConsoleKey.Q:
                        return input.KeyChar;
                }
            }
        }

        private static void AddUser()
        {
            string name;

            do
            {
                Console.WriteLine();
                Console.Write("Enter name: ");
                name = Console.ReadLine();

                if (name != "") break;

                Console.WriteLine("The name must not be empty.");

            } while (true);

            while (true)
            {
                Console.Write("Enter the date of birth(Example: 01.12.1970): ");

                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null,
                    DateTimeStyles.None, out DateTime dateOfBirth))
                {
                    if (UsersManager.AddUser(name, dateOfBirth))
                        Console.WriteLine($"User added successful.{Environment.NewLine}");
                    else
                        Console.WriteLine("User already exist.");

                    break;
                }

                Console.WriteLine("Enter the correct date of birth.");
                Console.WriteLine();
            }
        }

        private static void RemoveUser()
        {
            string name;

            do
            {
                Console.WriteLine();
                Console.Write("Enter name: ");
                name = Console.ReadLine();

                if (name != "") break;

                Console.WriteLine("The name must not be empty.");

            } while (true);

            if (UsersManager.RemoveUser(name))
                Console.WriteLine($"User was deleted.{Environment.NewLine}");
            else
                Console.WriteLine($"User not found with this name.{Environment.NewLine}");
        }

        private static void ShowUsers(IEnumerable<User> users)
        {
            if (users is ICollection<User> u && u.Count != 0)
            {
                Console.WriteLine();

                foreach (User user in u)
                {
                    Console.WriteLine($"User: {user.Name}{Environment.NewLine}"
                                      + $"ID: {user.Id}{Environment.NewLine}"
                                      + $"Date of Birth: {user.DateOfBirth.ToShortDateString()}{Environment.NewLine}"
                                      + $"Age: {user.Age}{Environment.NewLine}");
                }
            }
            else
            {
                Console.WriteLine($"{Environment.NewLine}Users not exists.");
                Console.WriteLine();
            }
        }
    }
}
