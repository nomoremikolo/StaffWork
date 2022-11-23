namespace StaffWork.Server.GraphQL.User.Inputs
{
    public class CreateUserInput
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Adress { get; set; }
        public string Role { get; set; } = "User";
        public List<int> Permissions { get; set; }
    }
}
