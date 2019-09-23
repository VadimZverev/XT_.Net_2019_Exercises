using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace EPAM.UsersAndAwards.DAL
{
    public class RoleFileDao : IRoleFileDao
    {
        private static readonly string _dataBase;
        private static readonly Dictionary<int, Role> _repoRoles;

        static RoleFileDao()
        {
            _dataBase =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["Roles"]);

            _repoRoles = new Dictionary<int, Role>();

            GetData();
        }

        private static void GetData()
        {
            if (!string.IsNullOrEmpty(_dataBase)
                && File.Exists(_dataBase))
            {
                string data = File.ReadAllText(_dataBase);

                var rolesDb = new { Roles = new List<Role>() };

                rolesDb = JsonConvert.DeserializeAnonymousType(data, rolesDb);

                foreach (Role role in rolesDb.Roles)
                {
                    _repoRoles.Add(role.Id, role);
                }
            }
        }

        public void Add(Role role)
        {
            var lastId = _repoRoles.Any()
                ? _repoRoles.Keys.Max()
                : 0;
            role.Id = ++lastId;

            _repoRoles.Add(role.Id, role);

            Save();
        }


        public bool Delete(int id)
        {
            if (_repoRoles.Remove(id))
            {
                Save();
                return true;
            }

            return false;
        }

        public IEnumerable<Role> GetAll()
        {
            foreach (var role in _repoRoles.Values)
            {
                yield return new Role
                {
                    Id = role.Id,
                    Name = role.Name
                };
            }
        }

        public Role GetById(int id)
        {
            return _repoRoles.TryGetValue(id, out var role)
                            ? new Role
                            {
                                Id = role.Id,
                                Name = role.Name
                            }
                            : null;
        }

        public bool Update(Role role)
        {
            if (_repoRoles.ContainsKey(role.Id))
            {
                _repoRoles[role.Id] = role;

                Save();

                return true;
            }

            return false;
        }

        private void Save()
        {
            if (!string.IsNullOrEmpty(_dataBase)
                && !string.IsNullOrWhiteSpace(_dataBase))
            {
                var roles = from r in _repoRoles.Values
                               select new
                               {
                                   r.Id,
                                   r.Name
                               };

                var db = new { Roles = roles };

                string dateBase = JsonConvert.SerializeObject(db, Formatting.Indented);

                File.WriteAllText(_dataBase, dateBase);
            }
        }
    }
}
