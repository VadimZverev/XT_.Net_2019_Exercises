using EPAM.UsersAndAwards.DAL.Interface;
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
                    ParameterName = "@Password",
                    Value = account.Password,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var role = new SqlParameter
                {
                    ParameterName = "@Role",
                    Value = account.Role,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.AddRange(new SqlParameter[]
                {
                    login,
                    password,
                    role
                });

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("DELETE FROM Accounts WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }

        public IEnumerable<Account> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Accounts";
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var login = (string)reader["Login"];
                    var password = (string)reader["Password"];
                    var role = (int)reader["Role"];

                    yield return new Account
                    {
                        Id = id,
                        Login = login,
                        Password = password,
                        Role = role
                    };
                }
            }
        }

        public Account GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM Accounts WHERE Id = {id};";

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var login = (string)reader["Login"];
                    var password = (string)reader["Password"];
                    var role = (int)reader["Role"];

                    return new Account
                    {
                        Id = id,
                        Login = login,
                        Password = password,
                        Role = role
                    };
                }

                return null;
            }
        }

        public bool Update(Account account)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var commandStr = "UPDATE Accounts SET " +
                                 "Login = @login, " +
                                 "Password = @password, " +
                                 "Role = @role " +
                                 "WHERE Id = @id;";

                using (var command = new SqlCommand(commandStr, connection))
                {
                    command.Parameters.AddWithValue("@id", account.Id);
                    command.Parameters.AddWithValue("@login", account.Login);
                    command.Parameters.AddWithValue("@password", account.Password);
                    command.Parameters.AddWithValue("@role", account.Role);

                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}
