using BusinessLogic.Models;

namespace StaffWork.Server.GraphQL.Ware.Output.Basket
{
    public class CRUDBasketResponse
    {
        public BasketWareGraph? Ware { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public CRUDBasketResponse()
        {
            Errors = new List<string>();
        }
    }
}
