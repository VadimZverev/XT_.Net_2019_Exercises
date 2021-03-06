﻿using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.Common;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EPAM.UsersAndAwards.ConsolePL
{
    class Program
    {
        private static readonly string _storageMode
            = ConfigurationManager.AppSettings["StorageMode"];

        private static IUserLogic _userLogic;
        private static IAwardLogic _awardLogic;
        private static IAwardUserLogic _awardUserLogic;

        static void Main()
        {
            InitialLogic(_storageMode);

            char select;
            Console.OutputEncoding = Encoding.Unicode;

            do
            {
                select = SelectOption();

            } while (select != 'q' && select != 'й');
        }

        private static void InitialLogic(string storageMode)
        {
            if (storageMode == "File")
            {
                _userLogic = DependencyResolver.UserFileLogic;
                _awardLogic = DependencyResolver.AwardFileLogic;
                _awardUserLogic = DependencyResolver.AwardUserFileLogic;
            }
            else if (storageMode == "DataBase")
            {
                _userLogic = DependencyResolver.UserDbLogic;
                _awardLogic = DependencyResolver.AwardDbLogic;
                _awardUserLogic = DependencyResolver.AwardUserDbLogic;
            }
            else
            {
                _userLogic = DependencyResolver.UserLogic;
                _awardLogic = DependencyResolver.AwardLogic;
                _awardUserLogic = DependencyResolver.AwardUserLogic;
            }
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

            if (_awardLogic.Add(new Award { Title = title }))
                Console.WriteLine($"Award added successful.{Environment.NewLine}");
            else
                Console.WriteLine("Award already exist.");
        }

        private static void AddAwardUser()
        {
            Award award;
            User user;
            string title;

            do
            {
                Console.WriteLine();
                Console.Write("Enter Id user: ");

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    user = _userLogic.GetById(id);

                    if (user != null)
                        break;

                    Console.WriteLine($"User was not found with this Id = {id}. Try again.");
                    continue;
                }

                Console.WriteLine("The Id must not be empty.");

            } while (true);

            do
            {
                Console.Write("Enter title: ");
                title = Console.ReadLine();

                if (!string.IsNullOrEmpty(title) || !string.IsNullOrWhiteSpace(title))
                {
                    award = _awardLogic.GetAll().FirstOrDefault(a => a.Title == title);

                    if (award != null)
                    {

                        if (_awardUserLogic.GetById(award.Id, user.Id) != null)
                        {
                            Console.WriteLine($"Award already exists, enter another award.");
                            continue;
                        }

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Award cannot found with this title. Try again.");
                        continue;
                    }
                }

                Console.WriteLine("The title must not be empty.");

            } while (true);

            AwardUser awardUser = new AwardUser
            {
                AwardId = award.Id,
                UserId = user.Id
            };

            if (_awardUserLogic.Add(awardUser))
            {
                Console.WriteLine($"Award \"{title}\" added to User \"{user.Name}\" successful.{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine("The award has not been added to the user.");
            }

            Console.WriteLine();
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
                    User user = new User { Name = name, DateOfBirth = dateOfBirth };

                    if (_userLogic.Add(user))
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

            var award = _awardLogic.GetAll().FirstOrDefault(a => a.Title == title);

            if (award != null)
            {
                _awardLogic.Delete(award.Id);

                var awardUserList = _awardUserLogic.GetAll()
                                                   .Where(au => au.AwardId == award.Id)
                                                   .ToList();

                foreach (var awardUser in awardUserList)
                {
                    _awardUserLogic.Delete(awardUser.AwardId, awardUser.UserId);
                }

                Console.WriteLine($"Award was deleted.{Environment.NewLine}");
            }
            else
                Console.WriteLine($"Award cannot found with this title.{Environment.NewLine}");
        }

        private static void RemoveAwardUser()
        {
            Award award;
            User user;

            do
            {
                Console.WriteLine();
                Console.Write("Enter Id user: ");


                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    user = _userLogic.GetById(id);

                    if (user != null)
                        break;

                    Console.WriteLine($"User was not found with this Id = {id}.");
                    continue;
                }

                Console.WriteLine("The Id must not be empty.");

            } while (true);

            do
            {
                Console.Write("Enter title: ");
                string title = Console.ReadLine();

                if (title != "")
                {
                    award = _awardLogic.GetAll().FirstOrDefault(a => a.Title == title);

                    if (!string.IsNullOrEmpty(title) || !string.IsNullOrWhiteSpace(title))
                        break;

                    Console.WriteLine($"Award cannot found with this title.");
                    continue;
                }

                Console.WriteLine("The title must not be empty.");

            } while (true);


            AwardUser awardUser = _awardUserLogic.GetById(award.Id, user.Id);

            if (awardUser != null
                && _awardUserLogic.Delete(award.Id, user.Id))
            {
                Console.WriteLine($"Award \"{award.Title}\" removed to " +
                                    $"User \"{user.Name}\" successful.{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine("The award has not been removed in the user.");
            }

            Console.WriteLine();
        }

        private static void RemoveUser()
        {
            int id;

            do
            {
                Console.WriteLine();
                Console.Write("Enter id user: ");

                if (int.TryParse(Console.ReadLine(), out id))
                    break;

                Console.WriteLine("Enter Id correctly.");

            } while (true);

            var user = _userLogic.GetById(id);

            if (user != null)
            {
                _userLogic.Delete(user.Id);

                var awardUserList = _awardUserLogic.GetAll()
                                                   .Where(au => au.UserId == user.Id)
                                                   .ToList();

                foreach (var awardUser in awardUserList)
                {
                    _awardUserLogic.Delete(awardUser.AwardId, awardUser.UserId);
                }

                Console.WriteLine($"User was deleted.{Environment.NewLine}");
            }
            else
                Console.WriteLine($"User was not found with this Id = {id}.{Environment.NewLine}");
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
                        AddAwardUser();
                        return input.KeyChar;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        RemoveAward();
                        return input.KeyChar;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        RemoveUser();
                        return input.KeyChar;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        RemoveAwardUser();
                        return input.KeyChar;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        ShowAwards(_awardLogic.GetAll());
                        return input.KeyChar;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        ShowUsers(_userLogic.GetAll());
                        return input.KeyChar;
                    case ConsoleKey.Q:
                        return input.KeyChar;
                }
            }
        }

        private static void ShowAwards(IEnumerable<Award> awards)
        {
            if (awards.Count() != 0)
            {
                Console.WriteLine("Awards:");

                foreach (Award award in awards)
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
            if (users.Count() != 0)
            {
                Console.WriteLine();

                foreach (User user in users)
                {
                    Console.WriteLine($"User: {user.Name}{Environment.NewLine}"
                                      + $"ID: {user.Id}{Environment.NewLine}"
                                      + $"Date of Birth: {user.DateOfBirth.ToShortDateString()}{Environment.NewLine}"
                                      + $"Age: {user.Age}");

                    var awardIds = _awardUserLogic.GetAll()
                                           .Where(ac => ac.UserId == user.Id)
                                           .Select(au => au.AwardId);

                    var awards = _awardLogic.GetAll()
                                    .Where(award => awardIds.Contains(award.Id));

                    ShowAwards(awards);

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