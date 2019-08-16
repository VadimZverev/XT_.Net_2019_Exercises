using System.Collections.Generic;
using System.Linq;
using Users_and_Awards.Entities;

namespace Users_and_Awards.DAL
{
    public class UsersStorage : IUserRepository
    {
        public UsersStorage()
        {
            Users = new List<User>();
        }

        public UsersStorage(ICollection<User> users)
        {
            Users = users.ToList();
        }

        private List<User> Users { get; set; }

        public bool AddUser(User user)
        {
            if (!Users.Exists(u => u.Id == user.Id))
            {
                Users.Add(user);
                return true;
            }

            return false;
        }

        public ICollection<User> GetAllUsers() => Users;

        public User GetUser(string name) =>
            Users.FirstOrDefault(u => u.Name == name);

        public bool RemoveUser(string name)
        {
            User user = GetUser(name);

            if (user == null)
                return false;

            return Users.Remove(user);
        }
    }
}
