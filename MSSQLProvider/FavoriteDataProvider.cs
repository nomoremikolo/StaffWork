using BusinessLogic;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Dapper;

namespace MSSQLProvider
{
    public class FavoriteDataProvider : IFavoriteDataProvider
    {
        private readonly string connectionString;
        public FavoriteDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public FavoriteWareModel AddToFavorite(NewFavoriteModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var isInFavorite = connection.QueryFirstOrDefault<FavoriteWareModel>(@"select * from Favorites where WareId = @WareId and UserId = @UserId", new
                {
                    @WareId = model.WareId,
                    @UserId = model.UserId,
                });
                if (isInFavorite != null)
                {
                    return isInFavorite;
                }
                var queryResult = connection.QueryFirstOrDefault(@"
                    insert into Favorites (WareId, UserId) values (@WareId, @UserId) SELECT SCOPE_IDENTITY() AS [Id];
                ", new
                {
                    @WareId = model.WareId,
                    @UserId = model.UserId,
                });

                var Id = (int)queryResult.Id;
                return GetFavoriteById(Id);
            }
        }

        public FavoriteWareModel GetFavoriteById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                return connection.QueryFirstOrDefault<FavoriteWareModel>(@"
                    select * from Ware t1 inner join Favorites t2 on t2.WareId = t1.Id and t2.Id = @Id
                ", new
                {
                    @Id = id,
                });
            }
        }

        public List<FavoriteWareModel> GetUserFavorite(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                return connection.Query<FavoriteWareModel>(@"
                    select WareId as Id, t2.Id as FavoriteId, Name, BrandId, CategoryId, Description, Sizes, Price, OldPrice, IsDiscount, CountInStorage, UserId, Thumbnail, Images from Ware t1 inner join Favorites t2 on t2.WareId = t1.Id and t2.UserId = @Id
                ", new
                {
                    @Id = userId,
                }).ToList();
            }
        }

        public FavoriteWareModel RemoveToFavorite(int userId, int wareId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                var result = connection.Execute(@"
                        delete from Favorites where UserId = @UserId and WareId = @WareId
                ", new
                {
                    @WareId = wareId,
                    @UserId = userId,
                });
                if (result < 1)
                {
                    throw new Exception("Error, please check your data..");
                }
                return null;
            }
        }
    }
}
