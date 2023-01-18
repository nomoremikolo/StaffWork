using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Ware.Output.Basket;
using StaffWork.Server.GraphQL.Ware.Output.Ware;

namespace StaffWork.Server.Providers.Interfaces
{
    public interface IBasketProvider
    {
        CRUDBasketResponse AddToBasket(NewBasketWareModel newBasketWare);
        CRUDBasketResponse ChangeBasketWareCount(NewBasketWareModel newBasketWare);
        CRUDBasketResponse RemoveFromBasket(int basketWareId);
        CRUDOrderResponse UpdateOrder(int id, string? status, bool? isConfirmed);
        CRUDBasketResponse ClearBasket();
        GetBasketWaresResponse GetAllBasketWares();
        CRUDBasketResponse GetBasketWareById(int wareId);
        GetBasketWaresResponse ConfirmOrder();
        List<OrderGraph> GetOrders(bool? confirmedFilter);
    }
}
