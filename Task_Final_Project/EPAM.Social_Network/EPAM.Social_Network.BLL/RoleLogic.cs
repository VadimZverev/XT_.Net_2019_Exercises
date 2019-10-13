using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using EPAM.Social_Network.Loggers;
using System;
using System.Collections.Generic;

namespace EPAM.Social_Network.BLL
{
    public class RoleLogic : IDbLogic<Role>
    {
        private readonly IDbDao<Role> _roleDao;

        public RoleLogic(IDbDao<Role> roleDao)
        {
            _roleDao = roleDao;
        }

        public int Add(Role entity)
        {
            try
            {
                Role role = _roleDao.GetById(entity.Id);

                if (role == null)
                {
                    return _roleDao.Add(entity);
                }
            }
            catch (Exception ex)
            {
                string message = "Failed to add role to DB.";
                Logger.SendError(ex, message);
            }

            return 0;
        }

        public bool Delete(int id)
        {
            try
            {
                return _roleDao.Delete(id);
            }
            catch (Exception ex)
            {
                string message = "Failed to delete role from DB.";
                Logger.SendError(ex, message);
            }

            return false;
        }

        public IEnumerable<Role> GetAll()
        {
            try
            {
                return _roleDao.GetAll();
            }
            catch (Exception ex)
            {
                string message = "Failed to get roles from DB.";
                Logger.SendError(ex, message);
            }

            return new Role[0];
        }

        public Role GetById(int id)
        {
            try
            {
                return _roleDao.GetById(id);
            }
            catch (Exception ex)
            {
                string message = "Failed to get role from DB.";
                Logger.SendError(ex, message);
            }

            return null;
        }

        public bool Update(Role entity)
        {
            try
            {
                return _roleDao.Update(entity);
            }
            catch (Exception ex)
            {
                string message = "Failed to update role.";
                Logger.SendError(ex, message);
            }

            return false;
        }
    }
}
