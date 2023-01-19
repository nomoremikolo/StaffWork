using StaffWork.Server.JwtAuthorization;

namespace StaffWork.Server.GraphQL.User.Output
{
    public class GetPermissionsQueryResponse
    {
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public List<PermissionModel> Permissions { get; set; }

        public GetPermissionsQueryResponse()
        {
            Errors = new List<string>();
        }
    }
}
