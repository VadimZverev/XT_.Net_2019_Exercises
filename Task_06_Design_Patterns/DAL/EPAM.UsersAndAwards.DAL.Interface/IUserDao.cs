using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.DAL.Interface
{
    public interface IUserDao
    {
        void Add(User user);

        bool Delete(int id);

        IEnumerable<User> GetAll();

        User GetById(int id);

        bool Update(User user);
    }
}
