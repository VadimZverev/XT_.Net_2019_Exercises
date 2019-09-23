using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.UsersAndAwards.DAL
{
    public class RoleFakeDao : IRoleDao
    {
        private static readonly Dictionary<int, Role> _repoRoles
            = new Dictionary<int, Role>();

        public void Add(Role role)
        {
            var lastId = _repoRoles.Any()
                            ? _repoRoles.Keys.Max()
                            : 0;
            role.Id = ++lastId;

            _repoRoles.Add(role.Id, role);
        }

        public bool Delete(int id)
        {
            return _repoRoles.Remove(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return _repoRoles.Values;
        }

        public Role GetById(int id)
        {
            return _repoRoles.TryGetValue(id, out var role)
                ? role
                : null;
        }

        public bool Update(Role role)
        {
            if (_repoRoles.ContainsKey(role.Id))
            {
                _repoRoles[role.Id] = role;
                return true;
            }

            return false;
        }
    }
}
