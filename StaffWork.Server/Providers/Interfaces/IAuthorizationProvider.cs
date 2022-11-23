using BusinessLogic.Models;
using StaffWork.Server.JwtAuthorization;

namespace StaffWork.Server.Providers.Interfaces
{
    public interface IAuthorizationProvider
    {
        AuthenticationResponseModel? Login(UserLoginModel user);
        AuthenticationResponseModel? LoginByRefreshToken(string token);
        AuthorizationResponse AuthorizeUser(HttpContext context, Func<UserModel, UserModel?> policy);
        UserModel? SignOut(UserModel user);
        List<PermissionModel> GetAllPermissions();
    }
}
