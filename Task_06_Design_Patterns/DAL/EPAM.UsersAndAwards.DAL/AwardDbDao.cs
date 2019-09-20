using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPAM.UsersAndAwards.DAL
{
    public class AwardDbDao : IAwardDbDao
    {
        private static readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public void Add(Award award)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddAward";
                command.CommandType = CommandType.StoredProcedure;

                var name = new SqlParameter
                {
                    ParameterName = "@Title",
                    Value = award.Title,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var image = new SqlParameter
                {
                    ParameterName = "@Image",
                    Value = award.Image ?? System.Data.SqlTypes.SqlBinary.Null,
                    SqlDbType = SqlDbType.VarBinary,
                    IsNullable = true,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.AddRange(new SqlParameter[]
                {
                    name,
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

                using (var command = new SqlCommand("DELETE FROM Awards WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }

            return true;
        }

        public IEnumerable<Award> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Awards";
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var title = (string)reader["Title"];
                    var image = reader["Image"] is DBNull ? null : (byte[])reader["Image"];

                    yield return new Award
                    {
                        Id = id,
                        Title = title,
                        Image = image
                    };
                }
            }
        }

        public Award GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM Awards WHERE Id = {id};";

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var title = (string)reader["Title"];
                    var image = reader["Image"] is DBNull ? null : (byte[])reader["Image"];

                    return new Award
                    {
                        Id = id,
                        Title = title,
                        Image = image
                    };
                }

                return null;
            }
        }

        public bool Update(Award award)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var commandStr = "UPDATE Awards SET " +
                                 "Title = @title, " +
                                 "Image = @image " +
                                 "WHERE Id = @id;";

                using (var command = new SqlCommand(commandStr, connection))
                {
                    command.Parameters.AddWithValue("@id", award.Id);
                    command.Parameters.AddWithValue("@title", award.Title);

                    var image = command.Parameters.Add("@image", SqlDbType.VarBinary);
                    image.Value = award.Image ?? System.Data.SqlTypes.SqlBinary.Null;
                    image.IsNullable = true;

                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}
