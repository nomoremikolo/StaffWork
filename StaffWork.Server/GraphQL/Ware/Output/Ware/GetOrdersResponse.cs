using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class GetOrdersResponse
    {
        public List<OrderGraph> Wares { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public GetOrdersResponse()
        {
            Errors = new List<string>();
        }
    }
}
