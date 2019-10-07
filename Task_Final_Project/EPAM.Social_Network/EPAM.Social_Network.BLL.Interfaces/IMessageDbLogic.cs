using EPAM.Social_Network.Entities;
using System.Collections.Generic;

namespace EPAM.Social_Network.BLL.Interfaces
{
    public interface IMessageDbLogic
    {
        bool Add(Message entity);

        bool Delete(int accountFromId, int accountToId);

        IEnumerable<Message> GetAll();

        bool Update(Message entity);
    }
}
