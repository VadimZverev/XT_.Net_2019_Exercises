using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace EPAM.Social_Network.DAL
{
    public class ProfileDao : IDbDao<Profile>
    {
        private static readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        public int Add(Profile entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "AddProfile";
                command.CommandType = CommandType.StoredProcedure;

                var firstName = new SqlParameter
                {
                    ParameterName = "@FirstName",
                    Value = entity.FirstName,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var middleName = new SqlParameter
                {
                    ParameterName = "@MiddleName",
                    Value = entity.MiddleName ?? SqlString.Null,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var lastName = new SqlParameter
                {
                    ParameterName = "@LastName",
                    Value = entity.LastName ?? SqlString.Null,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var male = new SqlParameter
                {
                    ParameterName = "@Male",
                    Value = entity.Male ?? SqlString.Null,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var dateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = entity.DateOfBirth ?? SqlDateTime.Null,
                    SqlDbType = SqlDbType.DateTime,
                    Direction = ParameterDirection.Input
                };

                var profilePhoto = new SqlParameter
                {
                    ParameterName = "@ProfilePhoto",
                    Value = entity.ProfilePhoto ?? SqlBinary.Null,
                    SqlDbType = SqlDbType.VarBinary,
                    Direction = ParameterDirection.Input
                };

                var city = new SqlParameter
                {
                    ParameterName = "@City",
                    Value = entity.City ?? SqlString.Null,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var id = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(firstName);
                command.Parameters.Add(middleName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(male);
                command.Parameters.Add(dateOfBirth);
                command.Parameters.Add(profilePhoto);
                command.Parameters.Add(city);
                command.Parameters.Add(id);

                connection.Open();
                command.ExecuteNonQuery();

                return (int)id.Value;
            }
        }

        public bool Delete(int id)
        {
            int res;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DeleteProfile";
                command.CommandType = CommandType.StoredProcedure;

                var profileId = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(profileId);

                connection.Open();
                res = command.ExecuteNonQuery();
            }

            return res > 0;
        }

        public IEnumerable<Profile> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetProfiles";
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var id = (int)reader["Id"];
                    var firstName = (string)reader["FirstName"];
                    var middleName = (string)reader["MiddleName"];
                    var lastName = (string)reader["LastName"];
                    var male = (string)reader["Male"];
                    var dateOfBirth = (DateTime)reader["DateOfBirth"];
                    var profilePhoto = reader["ProfilePhoto"] is DBNull ? null : (byte[])reader["Image"];
                    var city = (string)reader["City"];

                    yield return new Profile
                    {
                        Id = id,
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName,
                        Male = male,
                        DateOfBirth = dateOfBirth,
                        ProfilePhoto = profilePhoto,
                        City = city
                    };
                }
            }
        }

        public Profile GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "GetProfileById";
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
                    var firstName = (string)reader["FirstName"];
                    var middleName = reader["MiddleName"] is DBNull ? null : (string)reader["MiddleName"];
                    var lastName = reader["LastName"] is DBNull ? null : (string)reader["LastName"];
                    var male = reader["Male"] is DBNull ? null : (string)reader["Male"];
                    var dateOfBirth = reader["DateOfBirth"] is DBNull ? null : (DateTime?)reader["DateOfBirth"];
                    var profilePhoto = reader["ProfilePhoto"] is DBNull ? null : (byte[])reader["ProfilePhoto"];
                    var city = reader["City"] is DBNull ? null : (string)reader["City"];

                    return new Profile
                    {
                        Id = id,
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName,
                        Male = male,
                        DateOfBirth = dateOfBirth,
                        ProfilePhoto = profilePhoto,
                        City = city
                    };
                }

                return null;
            }
        }

        public bool Update(Profile entity)
        {
            int res;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UpdateProfileById";
                command.CommandType = CommandType.StoredProcedure;

                var id = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = entity.Id,
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input
                };

                var firstName = new SqlParameter
                {
                    ParameterName = "@FirstName",
                    Value = entity.FirstName,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var middleName = new SqlParameter
                {
                    ParameterName = "@MiddleName",
                    Value = entity.MiddleName ?? SqlString.Null,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var lastName = new SqlParameter
                {
                    ParameterName = "@LastName",
                    Value = entity.LastName ?? SqlString.Null,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var male = new SqlParameter
                {
                    ParameterName = "@Male",
                    Value = entity.Male ?? SqlString.Null,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                var dateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = entity.DateOfBirth ?? SqlDateTime.Null,
                    SqlDbType = SqlDbType.DateTime,
                    Direction = ParameterDirection.Input
                };

                var profilePhoto = new SqlParameter
                {
                    ParameterName = "@ProfilePhoto",
                    Value = entity.ProfilePhoto ?? SqlBinary.Null,
                    SqlDbType = SqlDbType.VarBinary,
                    Direction = ParameterDirection.Input
                };

                var city = new SqlParameter
                {
                    ParameterName = "@City",
                    Value = entity.City ?? SqlString.Null,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input
                };

                command.Parameters.Add(id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(middleName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(male);
                command.Parameters.Add(dateOfBirth);
                command.Parameters.Add(profilePhoto);
                command.Parameters.Add(city);

                connection.Open();
                res = command.ExecuteNonQuery();

                return res > 0;
            }
        }
    }
}
