using BusinessLogic.Models;

namespace StaffWork.Server.JwtAuthorization
{
    public class AuthenticationResponseModel
    {
        public UserModel? User { get; set; }
        public string? JwtToken { get; set; }
        public string? RefreshToken { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public AuthenticationResponseModel()
        {
            Errors = new List<string>();
        }
    }
}
