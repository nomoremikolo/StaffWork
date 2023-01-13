using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IBasketDataProvider
    {
        public BasketWareGraph AddToBasket(NewBasketWareModel newBasketWare);
        public BasketWareGraph ChangeBasketWareCount(NewBasketWareModel newBasketWare);
        public BasketWare RemoveWareFromBasket(int wareId, int userId);
        public List<BasketWareGraph> GetAllWaresFromBasket(int userId);
        public BasketWareGraph GetWareFromBasketById(int id, int userId);
        public List<BasketWareGraph> ConfirmOrder(int userId);
    }
}
