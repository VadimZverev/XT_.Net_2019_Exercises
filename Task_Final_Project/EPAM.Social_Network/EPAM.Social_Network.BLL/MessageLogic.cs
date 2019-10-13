using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using EPAM.Social_Network.Loggers;
using System;
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
            try
            {
                return _messageDao.Add(entity);
            }
            catch (Exception ex)
            {
                string message = "Failed to add account to DB.";
                Logger.SendError(ex, message);
            }

            return false;
        }

        public void Delete(int accountId)
        {
            try
            {
                _messageDao.Delete(accountId);
            }
            catch (Exception ex)
            {
                string message = "Failed to delete messages \"from\" and \"to\" of account from DB.";
                Logger.SendError(ex, message);
            }
        }

        public bool Delete(int accountFromId, int accountToId)
        {
            try
            {
                return _messageDao.Delete(accountFromId, accountToId);
            }
            catch (Exception ex)
            {
                string message = "Failed to delete message of account from DB.";
                Logger.SendError(ex, message);
            }

            return false;
        }

        public IEnumerable<Message> GetAll()
        {
            try
            {
                return _messageDao.GetAll();
            }
            catch (Exception ex)
            {
                string message = "Failed to get messages from DB.";
                Logger.SendError(ex, message);
            }

            return new Message[0];
        }

        public bool Update(Message entity)
        {
            try
            {
                return _messageDao.Update(entity);
            }
            catch (Exception ex)
            {
                string message = "Failed to update message.";
                Logger.SendError(ex, message);
            }

            return false;
        }
    }
}
