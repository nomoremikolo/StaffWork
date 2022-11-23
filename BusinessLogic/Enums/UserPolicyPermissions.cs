using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Enums
{
    public class UserPolicyPermissions
    {
        public static IDictionary<PermissionsEnum, string> Permissions { get; set; }
        public enum PermissionsEnum
        {
            GetUsers = 2,
            UsersCRUD = 3,

        }

        static UserPolicyPermissions()
        {
            Permissions = new Dictionary<PermissionsEnum, string>();
            Permissions.Add(PermissionsEnum.GetUsers, "See employers list");
            Permissions.Add(PermissionsEnum.UsersCRUD, "Create, delete and edit employers");
        }
    }
}
