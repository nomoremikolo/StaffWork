using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class GetAuthorizedUserWaresResponse
    {
        public List<AuthorizedUserWareModel> Wares { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public GetAuthorizedUserWaresResponse()
        {
            Errors = new List<string>();
        }
    }
}
