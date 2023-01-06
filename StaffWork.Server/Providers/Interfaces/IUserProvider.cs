using BusinessLogic.Models;
using StaffWork.Server.GraphQL.User.Types;

namespace StaffWork.Server.Providers.Interfaces
{
    public interface IUserProvider
    {
        List<UserModel> GetUsers();
        //UserModel? GetUserByUsername(string username);
        //CRUDUserResponse? GetUserById(int userId);
        //int GetCountOfUsers();
        //int GetCountOfUsersForSearch(string searchName);
        CRUDUserResponse CreateUser(NewUserModel user);
        //CRUDUserResponse UpdateUserInfo(UpdateUserInfoModel user);
        //CRUDUserResponse UpdateUserSelfInfo(UpdateUserInfoModel updatedUser, UserModel userInDb);
        //CRUDUserResponse UpdateUserSelfPassword(UpdateUserPasswordModel updatedPassword, UserModel userInDb);
        //CRUDUserResponse DeleteUser(int userId);
    }
}
