namespace StaffWork.Server.JwtAuthorization
{
    public class PermissionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PermissionModel(int id, string text)
        {
            Id = id;
            Name = text;
        }
    }
}
