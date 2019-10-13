using EPAM.Social_Network.Entities;
using System.Collections.Generic;

namespace EPAM.Social_Network.BLL.Interfaces
{
    public interface IMessageDbLogic
    {
        bool Add(Message entity);

        void Delete(int accountId);

        bool Delete(int accountFromId, int accountToId);

        IEnumerable<Message> GetAll();

        bool Update(Message entity);
    }
}
