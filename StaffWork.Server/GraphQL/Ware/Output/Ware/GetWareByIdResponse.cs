using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class GetWareByIdResponse
    {
        public WareModelWithBrandAndCategory? Ware { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public GetWareByIdResponse()
        {
            Errors = new List<string>();
        }
    }
}
