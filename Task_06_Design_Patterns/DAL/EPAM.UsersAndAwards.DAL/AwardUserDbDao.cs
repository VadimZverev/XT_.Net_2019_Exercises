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
            int res;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DeleteAwardUser";
                command.CommandType = CommandType.StoredProcedure;

                var AwardId = new SqlParameter
                {
                    ParameterName = "@awardId",
                    Value = awardId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var UserId = new SqlParameter
                {
                    ParameterName = "@userId",
                    Value = userId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(AwardId);
                command.Parameters.Add(UserId);

                connection.Open();
                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }

        public IEnumerable<AwardUser> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetAwardUser";
                command.CommandType = CommandType.StoredProcedure;
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

        public AwardUser GetById(int awardId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetAwardUserByIds";
                command.CommandType = CommandType.StoredProcedure;

                var AwardId = new SqlParameter
                {
                    ParameterName = "@awardId",
                    Value = awardId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var UserId = new SqlParameter
                {
                    ParameterName = "@userId",
                    Value = userId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(AwardId);
                command.Parameters.Add(UserId);

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
    }
}
