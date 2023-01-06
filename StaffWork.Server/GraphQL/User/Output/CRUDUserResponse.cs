using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.User.Types
{
    public class CRUDUserResponse
    {
        public UserModel? User { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }

        public CRUDUserResponse()
        {
            Errors = new List<string>();
        }
    }
}
