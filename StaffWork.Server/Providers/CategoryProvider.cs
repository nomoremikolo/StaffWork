using BusinessLogic;
using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Ware.Output.Category;
using StaffWork.Server.Providers.Interfaces;

namespace StaffWork.Server.Providers
{
    public class CategoryProvider : ICategoryProvider
    {
        private ICategoryDataProvider categoryDataProvider;
        //private IAuthorizationProvider authorizationProvider;
        //private IHttpContextAccessor httpContextAccessor;
        public CategoryProvider(ICategoryDataProvider categoryDataProvider)
        {
            this.categoryDataProvider = categoryDataProvider;
        }

        public CRUDCategoryResponse CreateCategory(NewCategoryModel newCategory)
        {
            var response = new CRUDCategoryResponse();

            try
            {
                response.Category = categoryDataProvider.CreateCategory(newCategory);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
                throw;
            }
        }

        public CRUDCategoryResponse DeleteCategory(int categoryId)
        {
            var response = new CRUDCategoryResponse();

            try
            {
                response.Category = categoryDataProvider.DeleteCategory(categoryId);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
                throw;
            }
        }

        public GetCategoriesResponse GetAllCategories()
        {
            var response = new GetCategoriesResponse();
            try
            {
                response.Categories = categoryDataProvider.GetAllCategories();
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
                throw;
            }
            
        }

        public CRUDCategoryResponse UpdateCategory(CategoryModel categoryModel)
        {
            var response = new CRUDCategoryResponse();

            try
            {
                response.Category = categoryDataProvider.UpdateCategory(categoryModel);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
                throw;
            }
        }
    }
}
