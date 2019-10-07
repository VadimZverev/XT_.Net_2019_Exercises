using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using System.Collections.Generic;
using System.Linq;

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
            Role role = _roleDao.GetById(entity.Id);

            if (role == null)
            {
                return _roleDao.Add(entity);
            }

            return 0;
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

        public bool Update(Role entity)
        {
            return _roleDao.Update(entity);
        }
    }
}
