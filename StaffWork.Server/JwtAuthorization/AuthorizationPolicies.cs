using BusinessLogic.Models;

namespace StaffWork.Server.JwtAuthorization
{
    public class AuthorizationPolicies
    {
        public static UserModel? Authorized(UserModel user)
        {
            return user;
        }
        //public static UserModel? GetUsers(UserModel user)
        //{
        //    var permissionKey = ((int)PermissionsEnum.GetUsers).ToString();

        //    if (user.Permissions.Split(" ").Contains(permissionKey))
        //    {
        //        return user;
        //    }
        //    return null;
        //}
        //public static UserModel? PartTimer(UserModel user)
        //{
        //    if (user.IsPartTimer)
        //    {
        //        return user;
        //    }
        //    return null;
        //}
        //public static UserModel? UsersCRUD(UserModel user)
        //{
        //    var permissionKey = ((int)PermissionsEnum.UsersCRUD).ToString();
        //    if (user.Permissions.Split(" ").Contains(permissionKey))
        //    {
        //        return user;
        //    }
        //    return null;
        //}
        //public static UserModel? TimerItemsCRUD(UserModel user)
        //{
        //    var permissionKey = ((int)PermissionsEnum.TimerItemsCRUD).ToString();
        //    if (user.Permissions.Split(" ").Contains(permissionKey))
        //    {
        //        return user;
        //    }
        //    return null;
        //}

        //public static UserModel? VacationsList(UserModel user)
        //{
        //    var permissionKey = ((int)PermissionsEnum.VacationsList).ToString();
        //    if (user.Permissions.Split(" ").Contains(permissionKey))
        //    {
        //        return user;
        //    }
        //    return null;
        //}
    }
}
