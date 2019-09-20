using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPAM.UsersAndAwards.DAL
{
    public class AwardUserDbDao : IAwardUserDbDao
    {
        private static readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public void Add(AwardUser awardUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddAwardUser";
                command.CommandType = CommandType.StoredProcedure;

                var awardId = new SqlParameter
                {
                    ParameterName = "@AwardId",
                    Value = awardUser.AwardId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var userId = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = awardUser.UserId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.AddRange(new SqlParameter[]
                {
                    awardId,
                    userId
                });

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool Delete(int awardId, int userId)
        {
            int rows;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("DELETE FROM AwardUser WHERE AwardId = @awardId AND UserId = @userId;", connection))
                {
                    command.Parameters.AddWithValue("@awardId", awardId);
                    command.Parameters.AddWithValue("@userId", userId);
                    rows = command.ExecuteNonQuery();
                }
            }

            return rows > 0;
        }

        public IEnumerable<AwardUser> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM AwardUser";
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var awardId = (int)reader["AwardId"];
                    var userId = (int)reader["UserId"];

                    yield return new AwardUser
                    {
                        AwardId = awardId,
                        UserId = userId
                    };
                }
            }
        }

        ///<exception cref="NotImplementedException"></exception>
        public AwardUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public AwardUser GetById(int awardId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM Award WHERE AwardId = {awardId}, UserId = {userId};";

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new AwardUser
                    {
                        AwardId = awardId,
                        UserId = userId
                    };
                }

                return null;
            }
        }

        public bool Update(AwardUser awardUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var commandStr = "UPDATE AwardUser SET " +
                                 "AwardId = @awardId, " +
                                 "UserId = @userId;";

                using (var command = new SqlCommand(commandStr, connection))
                {
                    command.Parameters.AddWithValue("@awardId", awardUser.AwardId);
                    command.Parameters.AddWithValue("@userId", awardUser.UserId);

                    command.ExecuteNonQuery();
                }
            }

            return true;
        }
    }
}
