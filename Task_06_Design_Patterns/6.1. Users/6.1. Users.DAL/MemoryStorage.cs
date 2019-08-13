using _61_Users.Entities;
using System.Collections.Generic;
using System.Linq;

namespace _61_Users.DAL
{
    public class MemoryStorage : IStorable
    {
        public MemoryStorage()
        {
            Users = new List<User>();
        }

        private static List<User> Users { get; set; }

        public int Count => Users.Count;

        public bool AddUser(User user)
        {
            if (!Users.Exists(u => u.Id == user.Id))
            {
                Users.Add(user);
                return true;
            }

            return false;
        }

        public ICollection<User> GetAllUsers()
        {
            return Users;
        }

        public bool RemoveUser(string name)
        {
            User user = Users.FirstOrDefault(u => u.Name == name);

            if (user == null)
                return false;

            return Users.Remove(user);
        }
    }
}
