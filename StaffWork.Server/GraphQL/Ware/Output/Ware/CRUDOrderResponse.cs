using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class CRUDOrderResponse
    {
        public OrderModel? Order { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public CRUDOrderResponse()
        {
            Errors = new List<string>();
        }
    }
}
