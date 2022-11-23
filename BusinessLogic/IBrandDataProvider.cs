using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IBrandDataProvider
    {
        public BrandModel CreateBrand(NewBrandModel newBrand);
        public BrandModel UpdateBrand(BrandModel updatedBrand);
        public BrandModel DeleteBrand(int brandId);
        public BrandModel GetBrandById(int brandId);
        public List<BrandModel> GetAllBrands();
    }
}
