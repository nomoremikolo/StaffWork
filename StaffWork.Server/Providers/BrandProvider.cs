using BusinessLogic;
using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Ware.Output.Brand;
using StaffWork.Server.GraphQL.Ware.Output.Brands;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.Providers.Interfaces;

namespace StaffWork.Server.Providers
{
    public class BrandProvider : IBrandProvider
    {
        IBrandDataProvider brandDataProvider;
        private IAuthorizationProvider authorizationProvider;
        private IHttpContextAccessor httpContextAccessor;
        public BrandProvider(IBrandDataProvider brandDataProvider, IAuthorizationProvider authorizationProvider, IHttpContextAccessor httpContextAccessor)
        {
            this.brandDataProvider = brandDataProvider;
            this.authorizationProvider = authorizationProvider;
            this.httpContextAccessor = httpContextAccessor;
        }

        public CRUDBrandResponse CreateBrand(NewBrandModel newBrandModel)
        {
            var response = new CRUDBrandResponse();

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            try
            {
                response.Brand = brandDataProvider.CreateBrand(newBrandModel);
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

        public CRUDBrandResponse DeleteBrand(int id)
        {
            var response = new CRUDBrandResponse();

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            try
            {
                response.Brand = brandDataProvider.DeleteBrand(id);
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

        public GetBrandsResponse GetAllBrands()
        {
            var response = new GetBrandsResponse();
            try
            {
                response.Brands = brandDataProvider.GetAllBrands();
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

        public CRUDBrandResponse GetBrandById(int id)
        {
            var response = new CRUDBrandResponse();

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            try
            {
                response.Brand = brandDataProvider.GetBrandById(id);
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

        public CRUDBrandResponse UpdateBrand(BrandModel brandModel)
        {
            var response = new CRUDBrandResponse();

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            try
            {
                response.Brand = brandDataProvider.UpdateBrand(brandModel);
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
