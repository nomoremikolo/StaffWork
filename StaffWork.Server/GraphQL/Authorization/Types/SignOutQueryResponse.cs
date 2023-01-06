using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Authorization.Types
{
    public class SignOutQueryResponse
    {
        public UserModel User { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public SignOutQueryResponse()
        {
            Errors = new List<string>();
        }
    }
}
