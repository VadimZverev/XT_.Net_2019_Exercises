using Users_and_Awards.BLL;
using Users_and_Awards.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Users_and_Awards.PL
{
    class Program
    {
        static void Main()
        {
            char select;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            do
            {
                select = SelectOption();

            } while (select != 'q' && select != 'й');

            UsersManager.Save();
        }


        private static char SelectOption()
        {
            Console.WriteLine("Please select some action:");
            Console.WriteLine("1. Add Award");
            Console.WriteLine("2. Add User");
            Console.WriteLine("3. Add Award to User");
            Console.WriteLine("4. Remove Award");
            Console.WriteLine("5. Remove User");
            Console.WriteLine("6. Remove Award to User");
            Console.WriteLine("7. Show Awards");
            Console.WriteLine("8. Show Users");
            Console.WriteLine("Q. Exit");
            Console.WriteLine();

            while (true)
            {
                var input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        AddAward();
                        return input.KeyChar;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        AddUser();
                        return input.KeyChar;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        AddAwardToUser();
                        return input.KeyChar;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        RemoveAward();
                        return input.KeyChar;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        RemoveUser();
                        return input.KeyChar;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        ShowAwards(UsersManager.GetAllAwards());
                        return input.KeyChar;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        ShowUsers(UsersManager.GetAllUsers());
                        return input.KeyChar;
                    case ConsoleKey.Q:
                        return input.KeyChar;
                }
            }
        }

        private static void AddAwardToUser()
        {
            string name;
            string title;

            do
            {
                Console.WriteLine();
                Console.Write("Enter name: ");
                name = Console.ReadLine();

                if (name != "") break;

                Console.WriteLine("The name must not be empty.");

            } while (true);

            do
            {
                Console.Write("Enter title: ");
                title = Console.ReadLine();

                if (title != "") break;

                Console.WriteLine("The title must not be empty.");

            } while (true);

            if (UsersManager.AddAwardToUser(name, title))
            {
                Console.WriteLine($"Award \"{title}\" added to User \"{name}\" successful.{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine("The award has not been added to the user.");
            }

            Console.WriteLine();
        }

        private static void AddAward()
        {
            string title;

            do
            {
                Console.WriteLine();
                Console.Write("Enter tile: ");
                title = Console.ReadLine();

                if (title != "") break;

                Console.WriteLine("The title must not be empty.");

            } while (true);

            if (UsersManager.AddAward(title))
                Console.WriteLine($"Award added successful.{Environment.NewLine}");
            else
                Console.WriteLine("Award already exist.");
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

        private static void RemoveAward()
        {
            string title;

            do
            {
                Console.WriteLine();
                Console.Write("Enter title: ");
                title = Console.ReadLine();

                if (title != "") break;

                Console.WriteLine("The name must not be empty.");

            } while (true);

            if (UsersManager.RemoveAward(title))
                Console.WriteLine($"Award was deleted.{Environment.NewLine}");
            else
                Console.WriteLine($"Award cannot found with this title.{Environment.NewLine}");

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
                Console.WriteLine($"User cannot found with this name.{Environment.NewLine}");
        }

        private static void ShowAwards(IEnumerable<Award> awards)
        {
            if (awards is ICollection<Award> a && a.Count != 0)
            {
                Console.WriteLine("Awards:");

                foreach (Award award in a)
                {
                    Console.WriteLine($"\t- {award.Title}");
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"{Environment.NewLine}Awards not exists.");
                Console.WriteLine();
            }
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
                                      + $"Age: {user.Age}");

                    if (user.Awards.Count != 0)
                        ShowAwards(user.Awards);

                    Console.WriteLine();
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
