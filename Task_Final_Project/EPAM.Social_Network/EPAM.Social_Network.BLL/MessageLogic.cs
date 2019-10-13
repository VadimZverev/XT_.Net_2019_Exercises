using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using System.Collections.Generic;

namespace EPAM.Social_Network.BLL
{
    public class MessageLogic : IMessageDbLogic
    {
        private readonly IMessageDbDao _messageDao;

        public MessageLogic(IMessageDbDao messageDao)
        {
            _messageDao = messageDao;
        }

        public bool Add(Message entity)
        {
            return _messageDao.Add(entity);
        }

        public void Delete(int accountId)
        {
            _messageDao.Delete(accountId);
        }

        public bool Delete(int accountFromId, int accountToId)
        {
            return _messageDao.Delete(accountFromId, accountToId);
        }

        public IEnumerable<Message> GetAll()
        {
            return _messageDao.GetAll();
        }

        public bool Update(Message entity)
        {
            return _messageDao.Update(entity);
        }
    }
}
