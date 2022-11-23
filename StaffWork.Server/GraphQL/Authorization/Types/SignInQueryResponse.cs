using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Authorization.Types
{
    public class SignInQueryResponse
    {
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public UserModel? User { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
