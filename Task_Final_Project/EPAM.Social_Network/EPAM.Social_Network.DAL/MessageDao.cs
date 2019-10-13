using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPAM.Social_Network.DAL
{
    public class MessageDao : IMessageDbDao
    {
        private static readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public bool Add(Message entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddMessage";
                command.CommandType = CommandType.StoredProcedure;

                var accountFromId = new SqlParameter
                {
                    ParameterName = "@AccountFromId",
                    Value = entity.AccountFromId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var accountToId = new SqlParameter
                {
                    ParameterName = "@AccountToId",
                    Value = entity.AccountToId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var text = new SqlParameter
                {
                    ParameterName = "@Text",
                    Value = entity.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var dateOfCreation = new SqlParameter
                {
                    ParameterName = "@DateOfCreation",
                    Value = entity.DateOfCreation,
                    SqlDbType = SqlDbType.DateTime,
                    Direction = ParameterDirection.Input
                };

                var isAdd = new SqlParameter
                {
                    ParameterName = "@IsAdd",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(accountFromId);
                command.Parameters.Add(accountToId);
                command.Parameters.Add(text);
                command.Parameters.Add(dateOfCreation);
                command.Parameters.Add(isAdd);

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
                command.CommandText = "DeleteMessagesByAccId";
                command.CommandType = CommandType.StoredProcedure;

                var accId = new SqlParameter
                {
                    ParameterName = "@AccountId",
                    Value = accountId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(accId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public bool Delete(int accountFromId, int accountToId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DeleteMessage";
                command.CommandType = CommandType.StoredProcedure;

                var isDelete = new SqlParameter
                {
                    ParameterName = "@IsDelete",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                var accFromId = new SqlParameter
                {
                    ParameterName = "@AccountFromId",
                    Value = accountFromId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var accToId = new SqlParameter
                {
                    ParameterName = "@AccountToId",
                    Value = accountToId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(isDelete);
                command.Parameters.Add(accFromId);
                command.Parameters.Add(accToId);


                connection.Open();
                command.ExecuteNonQuery();

                return (bool)isDelete.Value;
            }
        }

        public IEnumerable<Message> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetMessages";
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var accountFromId = (int)reader["AccountFromId"];
                    var accountToId = (int)reader["AccountToId"];
                    var text = (string)reader["Text"];
                    var dateOfCreation = (DateTime)reader["DateOfCreation"];

                    yield return new Message
                    {
                        AccountFromId = accountFromId,
                        AccountToId = accountToId,
                        Text = text,
                        DateOfCreation = dateOfCreation
                    };
                }
            }
        }

        public bool Update(Message entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UpdateMessageById";
                command.CommandType = CommandType.StoredProcedure;

                var accountFromId = new SqlParameter
                {
                    ParameterName = "@AccountFromId",
                    Value = entity.AccountFromId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var accountToId = new SqlParameter
                {
                    ParameterName = "@AccountToId",
                    Value = entity.AccountToId,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var text = new SqlParameter
                {
                    ParameterName = "@Text",
                    Value = entity.Text,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var dateOfCreation = new SqlParameter
                {
                    ParameterName = "@RoleId",
                    Value = entity.DateOfCreation,
                    SqlDbType = SqlDbType.DateTime,
                    Direction = ParameterDirection.Input
                };

                var isUpdate = new SqlParameter
                {
                    ParameterName = "@IsUpdate",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(accountFromId);
                command.Parameters.Add(accountToId);
                command.Parameters.Add(text);
                command.Parameters.Add(dateOfCreation);
                command.Parameters.Add(isUpdate);

                connection.Open();
                command.ExecuteNonQuery();

                return (bool)isUpdate.Value;
            }
        }
    }
}
