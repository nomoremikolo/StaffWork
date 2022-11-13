using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActivated { get; set; } = true;
        public int? Age { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public string Roles { get; set; } = "User";
        public string Permissions { get; set; }
    }
}
