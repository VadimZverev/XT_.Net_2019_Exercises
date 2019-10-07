using EPAM.Social_Network.Entities;
using System.Collections.Generic;

namespace EPAM.Social_Network.BLL.Interfaces
{
    public interface IFriendDbLogic
    {
        bool Add(Friend entity);

        bool Delete(int accountId, int friendId);

        IEnumerable<Friend> GetAll();

        Friend GetById(int accountId, int friendId);

        bool Update(Friend entity);
    }
}
