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
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActivated { get; set; } = true;
        public int? Age { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; } = "User";
        public string Permissions { get; set; }
    }
}
