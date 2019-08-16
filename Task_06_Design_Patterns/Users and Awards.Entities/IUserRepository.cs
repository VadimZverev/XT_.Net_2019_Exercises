using System.Collections.Generic;

namespace Users_and_Awards.Entities
{
    public interface IUserRepository
    {
        bool AddUser(User user);

        User GetUser(string name);

        ICollection<User> GetAllUsers();

        bool RemoveUser(string name);
    }
}
