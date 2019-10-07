using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPAM.Social_Network.DAL
{
    public class AccountDao : IDbDao<Account>
    {
        private static readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public int Add(Account entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddAccount";
                command.CommandType = CommandType.StoredProcedure;

                var login = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = entity.Login,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var password = new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    Value = entity.PasswordHash,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var profileId = new SqlParameter
                {
                    ParameterName = "@ProfileId",
                    Value = entity.ProfileId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var roleId = new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = entity.RoleId ?? System.Data.SqlTypes.SqlInt32.Null,
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true,
                    Direction = ParameterDirection.Input
                };

                var id = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(login);
                command.Parameters.Add(password);
                command.Parameters.Add(profileId);
                command.Parameters.Add(roleId);
                command.Parameters.Add(id);

                connection.Open();
                command.ExecuteNonQuery();

                return (int)id.Value;
            }
        }

        public bool Delete(int id)
        {
            int res;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DeleteAccount";
                command.CommandType = CommandType.StoredProcedure;

                var accountId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(accountId);

                connection.Open();
                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }

        public IEnumerable<Account> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetAccounts";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var login = (string)reader["Login"];
                    var passwordHash = (string)reader["PasswordHash"];
                    var profileId = (int)reader["ProfileId"];
                    var roleId = reader["RoleId"] is DBNull ? null : (int?)reader["RoleId"];

                    yield return new Account
                    {
                        Id = id,
                        Login = login,
                        PasswordHash = passwordHash,
                        ProfileId = profileId,
                        RoleId = roleId
                    };
                }
            }
        }

        public Account GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetAccountById";
                command.CommandType = CommandType.StoredProcedure;

                var accountId = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(accountId);

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var login = (string)reader["Login"];
                    var passwordHash = (string)reader["PasswordHash"];
                    var profileId = (int)reader["ProfileId"];
                    var roleId = reader["RoleId"] is DBNull ? null : (int?)reader["RoleId"];

                    return new Account
                    {
                        Id = id,
                        Login = login,
                        PasswordHash = passwordHash,
                        ProfileId = profileId,
                        RoleId = roleId
                    };
                }

                return null;
            }
        }

        public bool Update(Account entity)
        {
            int res;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UpdateAccountById";
                command.CommandType = CommandType.StoredProcedure;

                var id = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = entity.Id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var login = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = entity.Login,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var password = new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    Value = entity.PasswordHash,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var profileId = new SqlParameter
                {
                    ParameterName = "@ProfileId",
                    Value = entity.ProfileId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var roleId = new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = entity.RoleId ?? System.Data.SqlTypes.SqlInt32.Null,
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(id);
                command.Parameters.Add(login);
                command.Parameters.Add(password);
                command.Parameters.Add(profileId);
                command.Parameters.Add(roleId);

                connection.Open();
                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }
    }
}
