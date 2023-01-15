using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Basket
{
    public class GetBasketWaresResponse
    {
        public List<BasketWareGraph> Wares { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public GetBasketWaresResponse()
        {
            Errors = new List<string>();
        }
    }
}
