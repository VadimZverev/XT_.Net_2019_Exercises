using _61_Users.Common;
using _61_Users.Entities;
using System;
using System.Collections.Generic;

namespace _61_Users.BLL
{
    public static class UsersManager
    {
        public static IStorable MemoryStorage => Dependencies.UsersStorage;

        public static bool AddUser(string name, DateTime dateOfBirth)
        {
            return MemoryStorage.AddUser(
                        new User()
                        {
                            Name = name,
                            DateOfBirth = dateOfBirth
                        });
        }

        public static IEnumerable<User> GetAllUsers()
        {
            return MemoryStorage.GetAllUsers();
        }

        public static bool RemoveUser(string name)
        {
            return MemoryStorage.RemoveUser(name);
        }
    }
}
