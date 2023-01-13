using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output
{
    public class GetWaresResponse
    {
        public List<WareModel> Wares { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public GetWaresResponse()
        {
            Errors = new List<string>();
        }
    }
}
