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
    public class CategoryDataProvider : ICategoryDataProvider
    {
        private readonly string connectionString;
        public CategoryDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public CategoryModel CreateCategory(NewCategoryModel newCategory)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var queryResult = connection.QueryFirstOrDefault(@"
                    insert into [dbo].[Category] 
                    (Name)
                    VALUES 
                    (@Name)
                    SELECT SCOPE_IDENTITY() AS [Id];
                    ", new
                {
                    @Name = newCategory.Name,
                });
                var Id = (int)queryResult.Id;
                var addedBrand = GetCategoryById(Id);
                return addedBrand;
            }
        }

        public CategoryModel DeleteCategory(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var brand = GetCategoryById(categoryId);
                if (brand == null)
                {
                    return null;
                }
                connection.Open();
                var affectedRows = connection.Execute(
                    @"delete from [Category] where
                    where Id = @Id",
                    new
                    {
                        Id = categoryId
                    });
                return affectedRows > 0 ? brand : null;
            }
        }

        public List<CategoryModel> GetAllCategories()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<CategoryModel>(@"select * from [Category]").ToList();
            }
        }

        public CategoryModel GetCategoryById(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<CategoryModel>(@"select * from [Category] where Id = @id",
                    new
                    {
                        id = categoryId
                    });
            }
        }

        public CategoryModel UpdateCategory(CategoryModel updatedCategory)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = connection.Execute(
                    @"UPDATE [Category]
                    SET 
                    Name = @Name,
                    WHERE 
                    Id = @Id",
                    new
                    {
                        Id = updatedCategory.Id,
                        Name = updatedCategory.Name,
                    });
                if (affectedRows > 0)
                {
                    return GetCategoryById(updatedCategory.Id);
                }
            }
            return null;
        }
    }
}
