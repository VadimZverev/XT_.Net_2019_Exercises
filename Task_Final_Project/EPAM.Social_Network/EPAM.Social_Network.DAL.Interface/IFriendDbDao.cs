using EPAM.Social_Network.Entities;
using System.Collections.Generic;

namespace EPAM.Social_Network.DAL.Interfaces
{
    public interface IFriendDbDao
    {
        bool Add(Friend entity);

        void Delete(int accountId);

        bool Delete(int accountId, int friendId);

        IEnumerable<Friend> GetAll();

        Friend GetById(int accountId, int friendId);

        bool Update(Friend entity);
    }
}
