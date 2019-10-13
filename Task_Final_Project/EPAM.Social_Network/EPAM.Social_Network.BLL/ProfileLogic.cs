using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using EPAM.Social_Network.Loggers;
using System;
using System.Collections.Generic;

namespace EPAM.Social_Network.BLL
{
    public class ProfileLogic : IDbLogic<Profile>
    {
        private readonly IDbDao<Profile> _profileDao;

        public ProfileLogic(IDbDao<Profile> profileDao)
        {
            _profileDao = profileDao;
        }

        public int Add(Profile entity)
        {
            try
            {
                Profile profile = _profileDao.GetById(entity.Id);

                if (profile == null)
                {
                    return _profileDao.Add(entity);
                }
            }
            catch (Exception ex)
            {
                string message = "Failed to add profile to DB.";
                Logger.SendError(ex, message);
            }

            return 0;
        }

        public bool Delete(int id)
        {
            try
            {
                return _profileDao.Delete(id);
            }
            catch (Exception ex)
            {
                string message = "Failed to delete profile from DB.";
                Logger.SendError(ex, message);
            }

            return false;
        }

        public IEnumerable<Profile> GetAll()
        {
            try
            {
                return _profileDao.GetAll();
            }
            catch (Exception ex)
            {
                string message = "Failed to get profiles from DB.";
                Logger.SendError(ex, message);
            }

            return new Profile[0];
        }

        public Profile GetById(int id)
        {
            try
            {
                return _profileDao.GetById(id);
            }
            catch (Exception ex)
            {
                string message = "Failed to get profile from DB.";
                Logger.SendError(ex, message);
            }

            return null;
        }

        public bool Update(Profile entity)
        {
            try
            {
                return _profileDao.Update(entity);
            }
            catch (Exception ex)
            {
                string message = "Failed to update profile.";
                Logger.SendError(ex, message);
            }

            return false;
        }
    }
}
