using BusinessLogic.Models;
using static BusinessLogic.Enums.UserPolicyPermissions;

namespace StaffWork.Server.JwtAuthorization
{
    public class AuthorizationPolicies
    {
        public static UserModel? Authorized(UserModel user)
        {
            return user;
        }
        public static UserModel? GetUsers(UserModel user)
        {
            var permissionKey = ((int)PermissionsEnum.GetUsers).ToString();

            if (user.Permissions.Split(" ").Contains(permissionKey))
            {
                return user;
            }
            return null;
        }
        public static UserModel? WareCRUD(UserModel user)
        {
            var permissionKey = ((int)PermissionsEnum.WareCRUD).ToString();

            if (user.Permissions.Split(" ").Contains(permissionKey))
            {
                return user;
            }
            return null;
        }
        public static UserModel? UsersCRUD(UserModel user)
        {
            var permissionKey = ((int)PermissionsEnum.UsersCRUD).ToString();
            if (user.Permissions.Split(" ").Contains(permissionKey))
            {
                return user;
            }
            return null;
        }
    }
}
