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

                command.Parameters.Add(name);
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
                command.CommandText = "DeleteAward";
                command.CommandType = CommandType.StoredProcedure;

                var awardId = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(awardId);

                connection.Open();
                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }

        public IEnumerable<Award> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetAwards";
                command.CommandType = CommandType.StoredProcedure;

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
                command.CommandText = "GetAwardById";
                command.CommandType = CommandType.StoredProcedure;

                var awardId = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(awardId);

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
            int res;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UpdateAwardById";
                command.CommandType = CommandType.StoredProcedure;

                var id = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = award.Id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var title = new SqlParameter
                {
                    ParameterName = "@title",
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

                command.Parameters.Add(id);
                command.Parameters.Add(title);
                command.Parameters.Add(image);
                
                connection.Open();

                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }
    }
}
