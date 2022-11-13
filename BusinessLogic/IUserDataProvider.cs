using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IUserDataProvider
    {
        List<UserModel> GetUsers();
        UserModel? CreateUser(UserModel user);
        UserModel? GetUserByUsername(string username);
        UserModel? GetUserById(int id);
        UserModel? GetUserByRefreshToken(string token);
        UserModel? UpdateUser(UserModel user);
        UserModel? DeactivateUser(int id);
        int GetCountOfUsers();
        bool CheckRefreshTokenUniqueness(string token);
    }
}
