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

                command.Parameters.AddRange(new SqlParameter[]
                {
                    name,
                    dateOfBirth,
                    age,
                    image
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

                using (var command = new SqlCommand("DELETE FROM Users WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Users";
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
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM Users WHERE Id = {id};";

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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var commandStr = "UPDATE Users SET " +
                                 "Name = @name, " +
                                 "DateOfBirth = @dateOfBirth, " +
                                 "Age = @age, " +
                                 "Image = @image " +
                                 "WHERE Id = @id;";

                using (var command = new SqlCommand(commandStr, connection))
                {
                    command.Parameters.AddWithValue("@id", user.Id);
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@dateOfBirth", user.DateOfBirth);
                    command.Parameters.AddWithValue("@age", user.Age);

                    var image = command.Parameters.Add("@image", SqlDbType.VarBinary);
                    image.Value = user.Image ?? System.Data.SqlTypes.SqlBinary.Null;
                    image.IsNullable = true;

                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}
