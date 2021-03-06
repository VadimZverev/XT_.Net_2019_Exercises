﻿using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPAM.UsersAndAwards.DAL
{
    public class AccountDbDao : IAccountDbDao
    {
        private static readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public void Add(Account account)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddAccount";
                command.CommandType = CommandType.StoredProcedure;

                var login = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = account.Login,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var password = new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    Value = account.PasswordHash,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var role = new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = account.RoleId,
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(login);
                command.Parameters.Add(password);
                command.Parameters.Add(role);

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
                    var roleId = reader["RoleId"] is DBNull ? null : (int?)reader["RoleId"];

                    yield return new Account
                    {
                        Id = id,
                        Login = login,
                        PasswordHash = passwordHash,
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
                    var roleId = reader["RoleId"] is DBNull ? null : (int?)reader["RoleId"];

                    return new Account
                    {
                        Id = id,
                        Login = login,
                        PasswordHash = passwordHash,
                        RoleId = roleId
                    };
                }

                return null;
            }
        }

        public bool Update(Account account)
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
                    Value = account.Id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var login = new SqlParameter
                {
                    ParameterName = "@Login",
                    Value = account.Login,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var passwordHash = new SqlParameter
                {
                    ParameterName = "@PasswordHash",
                    Value = account.PasswordHash,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var roleId = new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = account.RoleId,
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(id);
                command.Parameters.Add(login);
                command.Parameters.Add(passwordHash);
                command.Parameters.Add(roleId);

                connection.Open();
                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }
    }
}
