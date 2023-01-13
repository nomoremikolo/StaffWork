using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Ware.Output;

namespace StaffWork.Server.Providers.Interfaces
{
    public interface IBasketProvider
    {
        CRUDBasketResponse AddToBasket(NewBasketWareModel newBasketWare);
        CRUDBasketResponse ChangeBasketWareCount(NewBasketWareModel newBasketWare);
        CRUDBasketResponse RemoveFromBasket(int basketWareId);
        GetBasketWaresResponse GetAllBasketWares();
        CRUDBasketResponse GetBasketWareById(int wareId);
        GetBasketWaresResponse ConfirmOrder();
    }
}
