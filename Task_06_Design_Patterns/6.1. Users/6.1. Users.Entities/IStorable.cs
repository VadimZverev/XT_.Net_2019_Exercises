using System.Collections.Generic;

namespace _61_Users.Entities
{
    public interface IStorable
    {
        bool AddUser(User user);

        ICollection<User> GetAllUsers();

        bool RemoveUser(string name);

        void Save();
    }
}
