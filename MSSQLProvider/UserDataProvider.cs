﻿using BusinessLogic;
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

        public List<UserModel> GetUsers(string? keyWords)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (keyWords != null)
                {
                    return connection.Query<UserModel>($@"select * from [User] where username like N'%{keyWords}%' or name like N'%{keyWords}%' or surname like N'%{keyWords}%' or adress like N'%{keyWords}%' or email like N'%{keyWords}%' or role like N'%{keyWords}%' order by name").ToList();
                }
                else
                {
                    return connection.Query<UserModel>(@"select * from [User] order by name").ToList();
                }

            }
        }

        public UserModel? UpdateUser(UserModel user)
        {
            var oldUser = GetUserById(user.Id);
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
                    RefreshToken = @RefreshToken,
                    IsActivated = @IsActivated
                    WHERE 
                    Id = @Id",
                    new
                    {
                        Id = user.Id,
                        Username = user.Username != null ? user.Username : oldUser.Username,
                        PasswordHash = user.PasswordHash != null ? user.PasswordHash : oldUser.PasswordHash,
                        PasswordSalt = user.PasswordSalt != null ? user.PasswordSalt : oldUser.PasswordSalt,
                        RefreshToken = user.RefreshToken != null ? user.RefreshToken : oldUser.RefreshToken,
                        Name = user.Name != null ? user.Name : oldUser.Name,
                        Surname = user.Surname != null ? user.Surname : oldUser.Surname,
                        IsActivated = user.IsActivated != null ? user.IsActivated : oldUser.IsActivated,
                        Age = user.Age != null ? user.Age : oldUser.Age,
                        Adress = user.Adress != null ? user.Adress : oldUser.Adress,
                        Email = user.Email != null ? user.Email : oldUser.Email,
                        Role = user.Role != null ? user.Role : oldUser.Role,
                        Permissions = user.Permissions != null ? user.Permissions : oldUser.Permissions,
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
