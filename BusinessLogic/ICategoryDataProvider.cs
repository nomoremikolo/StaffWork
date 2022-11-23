using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface ICategoryDataProvider
    {
        public CategoryModel CreateCategory(NewCategoryModel newCategory);
        public CategoryModel UpdateCategory(CategoryModel updatedCategory);
        public CategoryModel DeleteCategory(int categoryId);
        public CategoryModel GetCategoryById(int categoryId);
        public List<CategoryModel> GetAllCategories();
    }
}
