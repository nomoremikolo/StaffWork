using BusinessLogic;
using BusinessLogic.Enums;
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
    public class WareDataProvider : IWareDataProvider
    {
        private readonly string connectionString;
        public WareDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public WareModel CreateWare(NewWareModel newWare)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var queryResult = connection.QueryFirstOrDefault(@"
                    insert into [dbo].[Ware] 
                    (Name, BrandId, CategoryId, Description, Sizes, Price, OldPrice, IsDiscount, CountInStorage)
                    VALUES 
                    (@Name, @BrandId, @CategoryId, @Description, @Sizes, @Price, @OldPrice, @IsDiscount, @CountInStorage)
                    SELECT SCOPE_IDENTITY() AS [Id];
                    ", new
                {
                    Name = newWare.Name,
                    BrandId = newWare.BrandId,
                    CategoryId = newWare.CategoryId,
                    Description = newWare.Description,
                    Sizes = newWare.Sizes,
                    Price = newWare.Price,
                    OldPrice = newWare.OldPrice,
                    IsDiscount = newWare.IsDiscount,
                    CountInStorage = newWare.CountInStorage,
                });
                var Id = (int)queryResult.Id;
                var addedWare = GetWareById(Id);

                return addedWare;
            }
        }

        public WareModel DeleteWare(int wareId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var oldWare = GetWareById(wareId);
                connection.Open();
                var result = connection.Execute(
                    @"delete from [Ware] where Id = @Id
                    ", new
                    {
                        Id = wareId,
                    });

                return oldWare;
            }
        }

        public List<WareModel> GetAllWares(QuerySettings settings)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                var resultList = new List<WareModel>();
                string filter = "";
                switch (settings.Filter)
                {
                    case FilterEnum.Discount:
                        filter = settings.CategoryId != null ? " and IsDiscount = 1" : " where IsDiscount = 1";
                        break;
                    case FilterEnum.Novelty:
                        filter = settings.CategoryId != null ? $" and id > (select max(Id-{settings.CountOfRecords}) from Ware)" : $" where id > (select max(Id-{settings.CountOfRecords}) from Ware)";
                        break;
                    default:
                        break;
                }
                string reverse = "";
                string cl = "";
                if (settings.SortParam != null)
                {
                    reverse = settings.SortParam.IsReverse ? "DESC" : "";
                    cl = $"{settings.SortParam.Value} {reverse}";
                }
                else
                {
                    cl = "Id";
                }
                var Category = settings.CategoryId != null ? " where CategoryId = " + settings.CategoryId : "";
                resultList = connection.Query<WareModel>(
                @$"
                    select top (@countOfRecords) * from [Ware]{Category}{filter} ORDER BY {cl}
                ", new
                {
                    @countOfRecords = settings.CountOfRecords,
                }).ToList();
                
 
                
                return resultList;
            }
        }

        public List<AuthorizedUserWareModel> GetAllWaresWithFavorite(QuerySettings settings, int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //var ss = $"select top (@countOfRecords) * from [Ware]{Category}{filter} ORDER BY {cl}";
                var resultList = new List<AuthorizedUserWareModel>();
                string filter = "";
                switch (settings.Filter)
                {
                    case FilterEnum.Discount:
                        filter = settings.CategoryId != null ? " and IsDiscount = 1" : " where IsDiscount = 1";
                        break;
                    case FilterEnum.Novelty:
                        filter = settings.CategoryId != null ? $" and id > (select max(Id-{settings.CountOfRecords}) from Ware)" : $" where id > (select max(Id-{settings.CountOfRecords}) from Ware)";
                        break;
                    default:
                        break;
                }
                string reverse = "";
                string cl = "";
                if (settings.SortParam != null)
                {
                    reverse = settings.SortParam.IsReverse ? "DESC" : "";
                    cl = $"{settings.SortParam.Value} {reverse}";
                }
                else
                {
                    cl = "Id";
                }
                var Category = settings.CategoryId != null ? " where CategoryId = " + settings.CategoryId : "";
                resultList = connection.Query<AuthorizedUserWareModel>(
                @$"
                    select top (@countOfRecords) t2.IsFavorite, t1.Id, t1.Name, t1.BrandId, t1.CategoryId, t1.Description, t1.Sizes, t1.Price, t1.OldPrice, t1.IsDiscount, t1.CountInStorage from [Ware] t1 left join Favorites t2 on t1.Id = t2.WareId and t2.UserId = @UserId{Category}{filter} ORDER BY {cl}
                    ", new
                {
                    @countOfRecords = settings.CountOfRecords,
                    @UserId = userId,
                }).ToList();

                return resultList;
            }
        }
        public WareModel GetWareById(int wareId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.QueryFirstOrDefault<WareModel>(
                    @"select * from [Ware] where Id = @Id
                    ", new
                    {
                        Id = wareId
                    });
                return result;
            }
        }

        public List<WareModel> GetWaresByCategoryId(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<WareModel>(
                    @"select * from [Ware] where CategoryId = @Id
                    ", new
                    {
                        Id = categoryId,
                    }).ToList();

                return result;
            }
        }

        public WareModel UpdateWare(WareModel updatedWare)
        {
            throw new NotImplementedException();
        }
    }
}
