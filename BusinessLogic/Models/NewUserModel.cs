using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class NewUserModel
    {
        [Required(ErrorMessage = "Username : Username is required")]
        [RegularExpression(@"^[A-Za-z0-9_]+$", ErrorMessage = "Username : Please use only latin letters, numbers and _ in username")]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Username : Username must have at least 6 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password : Password is required")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Password : Please use only latin letters and numbers in password")]
        [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Password : Password must have at least 6 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Name : Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname : Surname is required")]
        public string Surname { get; set; }
        public int? Age { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; } = "User";
        public string Permissions { get; set; }

        public NewUserModel(string username, string password, string email, string name, string surname, string permissions)
        {
            Username = username;
            Password = password;
            Email = email;
            Name = name;
            Surname = surname;
            Permissions = permissions;
        }
    }
}
