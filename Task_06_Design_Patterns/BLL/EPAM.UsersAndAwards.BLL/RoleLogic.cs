using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.UsersAndAwards.BLL
{
    public class RoleLogic : IRoleLogic
    {
        private readonly IRoleDao _roleDao;

        public RoleLogic(IRoleDao roleDao)
        {
            _roleDao = roleDao;
        }

        public bool Add(Role role)
        {
            Role _role = _roleDao.GetById(role.Id);

            if (_role == null)
            {
                _roleDao.Add(role);
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            return _roleDao.Delete(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleDao.GetAll();
        }

        public Role GetById(int id)
        {
            return _roleDao.GetById(id);
        }

        public bool Update(Role role)
        {
            return _roleDao.Update(role);
        }
    }
}