using BusinessLogic;
using BusinessLogic.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLProvider
{
    public class UserDataProvider : IUserDataProvider
    {
        private readonly string connectionString;
        public UserDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public UserModel? CreateUser(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var queryResult = connection.QueryFirstOrDefault(@"
                    insert into [dbo].[User] 
                    (Username, PasswordHash, PasswordSalt, Name, Surname, IsActivated, Age, Adress, Email, Role, Permissions)
                    VALUES 
                    (@Username, @PasswordHash, @PasswordSalt, @Name, @Surname, @IsActivated, @Age, @Adress, @Email, @Role, @Permissions)
                    SELECT SCOPE_IDENTITY() AS [Id];
                    ", new
                {
                    Username = user.Username,
                    PasswordHash = user.PasswordHash,
                    PasswordSalt = user.PasswordSalt,
                    Name = user.Name,
                    Surname = user.Surname,
                    IsActivated = user.IsActivated,
                    Age = user.Age,
                    Adress = user.Adress,
                    Email = user.Email,
                    Role = user.Role,
                    Permissions = user.Permissions ?? "",
                });
                var Id = (int)queryResult.Id;
                var addedUser = GetUserById(Id);

                return addedUser;
            }
           
        }

        public bool CheckRefreshTokenUniqueness(string token)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<UserModel>(
                    @"select * from [User]
                    where RefreshToken = @RefreshToken",
                    new
                    {
                        RefreshToken = token
                    });

                if (result != null)
                {
                    return false;
                }
                return true;
            }
        }

        public UserModel? DeactivateUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var user = GetUserById(id);
                if (user == null)
                {
                    return null;
                }
                connection.Open();
                var affectedRows = connection.Execute(
                    @"update [User] set 
                    IsActivated = 0
                    where Id = @Id",
                    new
                    {
                        Id = id
                    });
                return affectedRows > 0 ? user : null;
            }
        }

        public int GetCountOfUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var count = connection.QueryFirstOrDefault<int>(
                    @"select count(*) from [User]");

                return count;
            }
        }

        public UserModel? GetUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<UserModel>(
                    @"select * from [User]
                    where Id = @Id",
                    new
                    {
                        Id = id
                    });

                return result;
            }
        }

        public UserModel? GetUserByRefreshToken(string token)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<UserModel>(
                    @"select * from [User]
                    where RefreshToken = @RefreshToken",
                    new
                    {
                        RefreshToken = token
                    });

                return result;
            }
        }

        public UserModel? GetUserByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<UserModel>(
                    @"select * from [User] where [Username] = @Username",
                    new
                    {
                        @Username = username
                    });
                return result;
            }
        }

        public List<UserModel> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<UserModel>(@"select * from [User] ORDER BY Name").ToList();
            }
        }

        public UserModel? UpdateUser(UserModel user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(
                    @"UPDATE [User]
                    SET 
                    Username = @Username, 
                    PasswordHash = @PasswordHash, 
                    PasswordSalt = @PasswordSalt,
                    Name = @Name,
                    Surname = @Surname,
                    Age = @Age,
                    Adress = @Adress,
                    Email = @Email,
                    Role = @Role,
                    Permissions = @Permissions,
                    RefreshToken = @RefreshToken
                    WHERE 
                    Id = @Id",
                    new
                    {
                        Id = user.Id,
                        Username = user.Username,
                        PasswordHash = user.PasswordHash,
                        PasswordSalt = user.PasswordSalt,
                        RefreshToken = user.RefreshToken,
                        Name = user.Name,
                        Surname = user.Surname,
                        IsActivated = user.IsActivated,
                        Age = user.Age,
                        Adress = user.Adress,
                        Email = user.Email,
                        Role = user.Role,
                        Permissions = user.Permissions ?? "",
                    });
                if (affectedRows > 0)
                {
                    return GetUserById(user.Id);
                }
            }
            return null;
        }
    }
}
