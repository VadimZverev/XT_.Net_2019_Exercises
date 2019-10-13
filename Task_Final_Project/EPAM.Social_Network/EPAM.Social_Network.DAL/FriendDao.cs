using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPAM.Social_Network.DAL
{
    public class FriendDao : IFriendDbDao
    {
        private static readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public bool Add(Friend entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddFriend";
                command.CommandType = CommandType.StoredProcedure;

                var isAdd = new SqlParameter
                {
                    ParameterName = "@IsAdd",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                var accountId = new SqlParameter
                {
                    ParameterName = "@AccountId",
                    Value = entity.AccountId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var friendId = new SqlParameter
                {
                    ParameterName = "@FriendId",
                    Value = entity.FriendId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var isAccept = new SqlParameter
                {
                    ParameterName = "@IsAccept",
                    Value = entity.IsAccept,
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(isAdd);
                command.Parameters.Add(accountId);
                command.Parameters.Add(friendId);
                command.Parameters.Add(isAccept);

                connection.Open();
                command.ExecuteNonQuery();

                return (bool)isAdd.Value;
            }
        }

        public void Delete(int accountId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DeleteFriendsByAccId";
                command.CommandType = CommandType.StoredProcedure;

                var _accountId = new SqlParameter
                {
                    ParameterName = "@AccountId",
                    Value = accountId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(_accountId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool Delete(int accountId, int friendId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DeleteFriend";
                command.CommandType = CommandType.StoredProcedure;

                var isDelete = new SqlParameter
                {
                    ParameterName = "@IsDelete",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                var _accountId = new SqlParameter
                {
                    ParameterName = "@AccountId",
                    Value = accountId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var _friendId = new SqlParameter
                {
                    ParameterName = "@FriendId",
                    Value = friendId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(isDelete);
                command.Parameters.Add(_accountId);
                command.Parameters.Add(_friendId);

                connection.Open();
                command.ExecuteNonQuery();

                return (bool)isDelete.Value;
            }
        }

        public IEnumerable<Friend> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetFriends";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var accountId = (int)reader["AccountId"];
                    var friendId = (int)reader["FriendId"];
                    var isAccept = (bool)reader["IsAccept"];

                    yield return new Friend
                    {
                        AccountId = accountId,
                        FriendId = friendId,
                        IsAccept = isAccept
                    };
                }
            }
        }

        public Friend GetById(int accountId, int friendId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetFriendById";
                command.CommandType = CommandType.StoredProcedure;

                var _accountId = new SqlParameter
                {
                    ParameterName = "@AccountId",
                    Value = accountId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var _friendId = new SqlParameter
                {
                    ParameterName = "@FriendId",
                    Value = friendId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(_accountId);
                command.Parameters.Add(_friendId);

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var accId = (int)reader["AccountId"];
                    var frndId = (int)reader["FriendId"];
                    var isAccept = (bool)reader["IsAccept"];

                    return new Friend
                    {
                        AccountId = accId,
                        FriendId = frndId,
                        IsAccept = isAccept
                    };
                }

                return null;
            }
        }

        public bool Update(Friend entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UpdateFriendById";
                command.CommandType = CommandType.StoredProcedure;

                var isUpdate = new SqlParameter
                {
                    ParameterName = "@IsUpdate",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                var accountId = new SqlParameter
                {
                    ParameterName = "@AccountId",
                    Value = entity.AccountId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var friendId = new SqlParameter
                {
                    ParameterName = "@FriendId",
                    Value = entity.FriendId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var isAccept = new SqlParameter
                {
                    ParameterName = "@IsAccept",
                    Value = entity.IsAccept,
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(isUpdate);
                command.Parameters.Add(accountId);
                command.Parameters.Add(friendId);
                command.Parameters.Add(isAccept);

                connection.Open();
                command.ExecuteNonQuery();

                return (bool)isUpdate.Value;
            }
        }
    }
}
