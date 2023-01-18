namespace StaffWork.Server.GraphQL.User.Input
{
    public class UpdateUserByAdminInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public bool IsActivated { get; set; }
        public string Permissions { get; set; }
        public string Role { get; set; }
        public string? Adress { get; set; }
    }
}
