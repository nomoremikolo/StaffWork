using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Ware.Output.Category;

namespace StaffWork.Server.Providers.Interfaces
{
    public interface ICategoryProvider
    {
        CRUDCategoryResponse CreateCategory(NewCategoryModel newCategory);
        CRUDCategoryResponse DeleteCategory(int categoryId);
        CRUDCategoryResponse UpdateCategory(CategoryModel categoryModel);
        GetCategoriesResponse GetAllCategories();
    }
}
