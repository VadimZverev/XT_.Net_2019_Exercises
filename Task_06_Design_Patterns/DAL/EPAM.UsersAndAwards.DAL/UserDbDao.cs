using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPAM.UsersAndAwards.DAL
{
    public class UserDbDao : IUserDbDao
    {
        private static readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public void Add(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddUser";
                command.CommandType = CommandType.StoredProcedure;

                var name = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = user.Name,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var dateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = user.DateOfBirth,
                    SqlDbType = SqlDbType.Date,
                    Direction = ParameterDirection.Input
                };

                var age = new SqlParameter
                {
                    ParameterName = "@Age",
                    Value = user.Age,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var image = new SqlParameter
                {
                    ParameterName = "@Image",
                    Value = user.Image ?? System.Data.SqlTypes.SqlBinary.Null,
                    SqlDbType = SqlDbType.VarBinary,
                    IsNullable = true,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(name);
                command.Parameters.Add(dateOfBirth);
                command.Parameters.Add(age);
                command.Parameters.Add(image);

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
                command.CommandText = "DeleteUser";
                command.CommandType = CommandType.StoredProcedure;

                var userId = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(userId);

                connection.Open();
                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetUsers";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var name = (string)reader["Name"];
                    var dateOfBirth = (DateTime)reader["dateOfBirth"];
                    var image = reader["Image"] is DBNull ? null : (byte[])reader["Image"];

                    yield return new User
                    {
                        Id = id,
                        Name = name,
                        DateOfBirth = dateOfBirth,
                        Image = image
                    };
                }
            }
        }

        public User GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetUserById";
                command.CommandType = CommandType.StoredProcedure;

                var userId = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(userId);

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var name = (string)reader["Name"];
                    var dateOfBirth = (DateTime)reader["dateOfBirth"];
                    var image = reader["Image"] is DBNull ? null : (byte[])reader["Image"];

                    return new User
                    {
                        Id = id,
                        Name = name,
                        DateOfBirth = dateOfBirth,
                        Image = image
                    };
                }

                return null;
            }
        }

        public bool Update(User user)
        {
            int res;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UpdateUserById";
                command.CommandType = CommandType.StoredProcedure;

                var id = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = user.Id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var name = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = user.Name,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var dateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = user.DateOfBirth,
                    SqlDbType = SqlDbType.Date,
                    Direction = ParameterDirection.Input
                };

                var age = new SqlParameter
                {
                    ParameterName = "@Age",
                    Value = user.Age,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var image = new SqlParameter
                {
                    ParameterName = "@Image",
                    Value = user.Image ?? System.Data.SqlTypes.SqlBinary.Null,
                    SqlDbType = SqlDbType.VarBinary,
                    IsNullable = true,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(id);
                command.Parameters.Add(name);
                command.Parameters.Add(dateOfBirth);
                command.Parameters.Add(age);
                command.Parameters.Add(image);

                connection.Open();

                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }
    }
}
