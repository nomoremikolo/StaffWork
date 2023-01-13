using BusinessLogic;
using BusinessLogic.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLProvider
{
    public class BasketDataProvider : IBasketDataProvider
    {
        private readonly string connectionString;
        public BasketDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public BasketWareGraph AddToBasket(NewBasketWareModel newBasketWare)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var basket = connection.QueryFirstOrDefault<BasketModel>(@"
                    select * from Basket where UserId = @UserId 
                ", new {
                    UserId = newBasketWare.UserId
                });
                if (basket == null)
                {
                    var basketId = connection.QueryFirstOrDefault(@"insert into Basket (UserId) values (@UserId) SELECT SCOPE_IDENTITY() AS [Id];", new
                    {
                        UserId = newBasketWare.UserId
                    });
                    newBasketWare.BasketId = (int)basketId;
                }
                else
                {
                    newBasketWare.BasketId = basket.Id;
                }
                var isWareInBasket = connection.QueryFirstOrDefault<BasketWare>(@"select * from BasketWare where WareId = @wareId", new
                {
                    wareId = newBasketWare.WareId
                });

                if (isWareInBasket == null)
                {
                    connection.QueryFirstOrDefault<BasketWare>(@"
                    insert into [dbo].[BasketWare] 
                    (WareId,BasketId,Count)
                    VALUES 
                    (@WareId,@BasketId,@Count)
                    SELECT SCOPE_IDENTITY() AS [Id];
                    ", new
                    {
                        @WareId = newBasketWare.WareId,
                        @BasketId = newBasketWare.@BasketId,
                        @Count = newBasketWare.Count,
                    });
                    var addedWare = GetWareFromBasketById(newBasketWare.WareId, newBasketWare.UserId);
                    return addedWare;
                }
                else
                {
                    connection.QueryFirstOrDefault<BasketWare>(@"
                    update basketware set count = (select count from BasketWare where WareId = @WareId)+@Count where WareId = @WareId and BasketId = @BasketId
                    ", new
                    {
                        @WareId = newBasketWare.WareId,
                        @BasketId = newBasketWare.@BasketId,
                        @Count = newBasketWare.Count,
                    });
                    var addedWare = GetWareFromBasketById(newBasketWare.WareId, newBasketWare.UserId);
                    return addedWare;
                }

            }
        }

        public BasketWareGraph ChangeBasketWareCount(NewBasketWareModel newBasketWare)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var basket = connection.QueryFirstOrDefault<BasketModel>(@"
                    select * from Basket where UserId = @UserId 
                ", new
                {
                    UserId = newBasketWare.UserId
                });
                newBasketWare.BasketId = basket.Id;

                    connection.QueryFirstOrDefault<BasketWare>(@"
                    update basketware set count = @Count where WareId = @WareId and BasketId = @BasketId
                    ", new
                    {
                        @WareId = newBasketWare.WareId,
                        @BasketId = newBasketWare.@BasketId,
                        @Count = newBasketWare.Count,
                    });
                    var addedWare = GetWareFromBasketById(newBasketWare.WareId, newBasketWare.UserId);
                    return addedWare;
            }
        }

        public List<BasketWareGraph> ConfirmOrder(int userId)
        {
            var wares = GetAllWaresFromBasket(userId);
            if (wares.Count < 1)
            {
                throw new Exception("The basket is empty!");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var orderId = connection.QueryFirstOrDefault(@"insert into [Order] (UserId, Status, IsConfirmed) values (@UserId, @Status, @IsConfirmed) SELECT SCOPE_IDENTITY() AS [Id];", new
                {
                    @UserId = userId,
                    @Status = "Очікує підтвердження",
                    @IsConfirmed = false,
                });
                var wareList = new List<string>();
                foreach (var item in wares)
                {
                    wareList.Add($"({item.Id}, {(int)orderId.Id}, {item.Count})");
                }
                var AffectedRows = connection.Execute($@"insert into OrderWare (WareId,OrderId,Count) values 
                    {string.Join(",", wareList)}
                ");
                if (AffectedRows > 0)
                {
                    connection.Execute(@"delete from BasketWare where BasketId = (select Id from Basket where UserId = @UserId)", new
                    {
                        @UserId = userId,
                    });
                }
                return wares;
            }
        }

        public List<BasketWareGraph> GetAllWaresFromBasket(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<BasketWareGraph>(@"select t1.BasketId,t1.Count, t2.Id, t2.Name, t2.BrandId, t2.CategoryId, t2.Description, t2.Sizes, t2.Price, t2.OldPrice, t2.IsDiscount, t2.CountInStorage from [BasketWare] t1 inner join Ware t2 on t2.Id = t1.WareId and t1.BasketId = (select Id from Basket where UserId = @UserId)", new
                {
                    UserId = userId,
                }).ToList();
            }
        }

        public BasketWareGraph GetWareFromBasketById(int id, int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<BasketWareGraph>(@"select t1.BasketId,t1.Count, t2.Id, t2.Name, t2.BrandId, t2.CategoryId, t2.Description, t2.Sizes, t2.Price, t2.OldPrice, t2.IsDiscount, t2.CountInStorage from [BasketWare] t1 inner join Ware t2 on t2.Id = @WareId and BasketId = (select Id from Basket where UserId = @UserId)",
                    new
                    {
                        WareId = id,
                        UserId = userId,
                    });
                return result;
            }
        }

        public BasketWare RemoveWareFromBasket(int wareId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(
                    @"delete from [BasketWare]
                    where WareId = @WareId and BasketId = (select Id from Basket where UserId = @UserId)",
                    new
                    {
                        WareId = wareId,
                        UserId = userId,
                    });
                return null;
            }
        }
    }
}
