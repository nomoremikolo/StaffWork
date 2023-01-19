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
            WareCRUD = 1,
            GetUsers = 2,
            UsersCRUD = 3,

        }

        static UserPolicyPermissions()
        {
            Permissions = new Dictionary<PermissionsEnum, string>();
            Permissions.Add(PermissionsEnum.WareCRUD, "Create, delete, edit wares");
            Permissions.Add(PermissionsEnum.GetUsers, "See users list");
            Permissions.Add(PermissionsEnum.UsersCRUD, "Deactivate and edit users");
        }
    }
}
