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
                    (WareId,BasketId,Count,Size)
                    VALUES 
                    (@WareId,@BasketId,@Count, @Size)
                    SELECT SCOPE_IDENTITY() AS [Id];
                    ", new
                    {
                        @WareId = newBasketWare.WareId,
                        @BasketId = newBasketWare.@BasketId,
                        @Count = newBasketWare.Count,
                        @Size = newBasketWare.Size,
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

        public BasketWareGraph ClearBasket(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(@"delete from BasketWare where BasketId = (select Id from Basket where UserId = @UserId)", new
                {
                    UserId = userId,
                });
                return null;
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
                    @Status = "Waiting accept",
                    @IsConfirmed = false,
                });
                var wareList = new List<string>();
                foreach (var item in wares)
                {
                    wareList.Add($"({item.Id}, {(int)orderId.Id}, {item.Count}, {item.Size})");
                }
                var AffectedRows = connection.Execute($@"insert into OrderWare (WareId,OrderId,Count,Size) values 
                    {string.Join(",", wareList)}
                ");
                if (AffectedRows > 0)
                {
                    connection.Execute(@"delete from BasketWare where BasketId = (select Id from Basket where UserId = @UserId)", new
                    {
                        @UserId = userId,
                    });
                    foreach (var item in wares)
                    {
                        connection.Execute(@"update Ware set CountInStorage = (select CountInStorage from Ware where Id = @WareId)-@Count where Id = @WareId", new
                        {
                            @WareId = item.Id,
                            @Count = item.Count
                        });
                    }

                }
                return wares;
            }
        }

        public List<BasketWareGraph> GetAllWaresFromBasket(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<BasketWareGraph>(@"select t1.BasketId,t1.Count, t2.Id, t2.Name, t2.BrandId, t2.CategoryId, t2.Description, t2.Sizes,t1.Size, t2.Price, t2.OldPrice, t2.IsDiscount, t2.CountInStorage, t2.Thumbnail, t2.Images from [BasketWare] t1 inner join Ware t2 on t2.Id = t1.WareId and t1.BasketId = (select Id from Basket where UserId = @UserId)", new
                {
                    UserId = userId,
                }).ToList();
                return result;
            }
        }

        public OrderModel GetOrderById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<OrderModel>(@"select * from [Order] where Id = @Id", new
                {
                    Id = id,
                });
            }
        }

        public List<OrderGraph> GetOrders(bool? confirmedFilter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var filter = confirmedFilter != null ? $@" where IsConfirmed = {(confirmedFilter == true ? $"1" : $"0")}" : "";
                var orders = connection.Query<OrderGraph>($@"select * from [Order]{filter}").ToList();

                for (int i = 0; i < orders.Count; i++)
                {
                    var result = connection.Query<OrderGraphModel>(@"select t1.Id as WareId, t1.Name as WareName, t4.Name as BrandName, t5.Name as CategoryName, t1.Description, t1.Sizes, t1.Price, t1.OldPrice, t1.IsDiscount, t1.CountInStorage, t2.OrderId, t2.Count, t2.Size from Ware t1 inner join OrderWare t2 on t2.WareId = t1.Id inner join Brand t4 on t1.BrandId = t4.Id inner join Category t5 on t1.CategoryId = t5.Id where t2.OrderId = @OrderId", new
                    {
                        @OrderId = orders[i].Id,
                    }).ToList();
                    orders[i].OrderWares.AddRange(result);
                }
                orders.Reverse();
                return orders;
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

        public OrderModel UpdateOrder(int id, string? status, bool? isConfirmed)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var oldOrder = GetOrderById(id);
                connection.Execute(@"update [Order] set Status = @Status, IsConfirmed = @IsConfirmed where Id = @Id",
                    new
                    {
                        Id = id,
                        Status = status != null ? status : oldOrder.Status,
                        IsConfirmed = isConfirmed != null ? isConfirmed : oldOrder.IsConfirmed,
                    });
                return GetOrderById(id);
            }
        }
    }
}
