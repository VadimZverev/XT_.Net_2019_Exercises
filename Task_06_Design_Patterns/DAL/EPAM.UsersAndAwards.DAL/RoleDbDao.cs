using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPAM.UsersAndAwards.DAL
{
    public class RoleDbDao : IRoleDbDao
    {
        private static readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public void Add(Role role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddRole";
                command.CommandType = CommandType.StoredProcedure;

                var name = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = role.Name,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(name);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool Delete(int id)
        {
            int res;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DeleteRole";
                command.CommandType = CommandType.StoredProcedure;

                var roleId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(roleId);

                connection.Open();
                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }

        public IEnumerable<Role> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetRoles";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var name = (string)reader["Name"];

                    yield return new Role
                    {
                        Id = id,
                        Name = name
                    };
                }
            }
        }

        public Role GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetRoleById";
                command.CommandType = CommandType.StoredProcedure;

                var roleId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(roleId);

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {   
                    var name = (string)reader["Name"];

                    return new Role
                    {
                        Id = id,
                        Name = name
                    };
                }

                return null;
            }
        }

        public bool Update(Role role)
        {
            int res;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UpdateRoleById";
                command.CommandType = CommandType.StoredProcedure;

                var id = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = role.Id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var login = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = role.Name,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(id);
                command.Parameters.Add(login);

                connection.Open();
                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }
    }
}
