using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.User.Types
{
    public class GetUsersResponse
    {
        public List<UserModel> Users { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public GetUsersResponse()
        {
            Errors = new List<string>();
        }
    }
}
