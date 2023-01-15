using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Ware.Output.Brand;
using StaffWork.Server.GraphQL.Ware.Output.Brands;

namespace StaffWork.Server.Providers.Interfaces
{
    public interface IBrandProvider
    {
        CRUDBrandResponse CreateBrand(NewBrandModel newBrandModel);
        CRUDBrandResponse DeleteBrand(int id);
        CRUDBrandResponse UpdateBrand(BrandModel brandModel);
        GetBrandsResponse GetAllBrands();
        CRUDBrandResponse GetBrandById(int id);
    }
}
