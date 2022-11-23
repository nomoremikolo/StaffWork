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
    public class BrandDataProvider : IBrandDataProvider
    {
        private readonly string connectionString;
        public BrandDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public BrandModel CreateBrand(NewBrandModel newBrand)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var queryResult = connection.QueryFirstOrDefault(@"
                    insert into [dbo].[Brand] 
                    (Name,CountryManufactured,Phone)
                    VALUES 
                    (@Name,@CountryManufactured,@Phone)
                    SELECT SCOPE_IDENTITY() AS [Id];
                    ", new
                {
                    @Name = newBrand.Name,
                    @CountryManufactured = newBrand.CountryManufactured,
                    @Phone = newBrand.Phone,
                });
                var Id = (int)queryResult.Id;
                var addedBrand = GetBrandById(Id);
                return addedBrand;
            }
        }

        public BrandModel DeleteBrand(int brandId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var brand = GetBrandById(brandId);
                if (brand == null)
                {
                    return null;
                }
                connection.Open();
                var affectedRows = connection.Execute(
                    @"delete from [Brand] where
                    where Id = @Id",
                    new
                    {
                        Id = brandId
                    });
                return affectedRows > 0 ? brand : null;
            }
        }

        public List<BrandModel> GetAllBrands()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<BrandModel>(@"select * from [Brand]").ToList();
            }
        }

        public BrandModel GetBrandById(int brandId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<BrandModel>(@"select * from [Brand] where Id = @id", 
                    new 
                    {
                        id = brandId
                    });
            }
        }

        public BrandModel UpdateBrand(BrandModel updatedBrand)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(
                    @"UPDATE [Brand]
                    SET 
                    Name = @Name, 
                    CountryManufactured = @ContryManufactured, 
                    Phone = @PasswordSalt
                    WHERE 
                    Id = @Id",
                    new
                    {
                        Id = updatedBrand.Id,
                        Name = updatedBrand.Name,
                        CountryManufactured = updatedBrand.CountryManufactured,
                        Phone = updatedBrand.Phone,
                    });
                if (affectedRows > 0)
                {
                    return GetBrandById(updatedBrand.Id);
                }
            }
            return null;
        }
    }
}
