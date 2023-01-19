namespace StaffWork.Server.GraphQL.User.Input
{
    public class UpdateBySelfInput
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Adress { get; set; }
    }
}
