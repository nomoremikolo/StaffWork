using BusinessLogic.Models;

namespace StaffWork.Server.JwtAuthorization
{
    public class AuthorizationResponse
    {
        public UserModel? User { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public AuthorizationResponse()
        {
            Errors = new List<string>();
        }
    }
}
