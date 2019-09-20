using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.BLL.Interface
{
    public interface IUserLogic
    {
        bool Add(User user);

        bool Delete(int id);

        IEnumerable<User> GetAll();

        User GetById(int id);

        bool Update(User user);
    }
}
