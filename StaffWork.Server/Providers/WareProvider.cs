using AutoMapper;
using BusinessLogic;
using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Authorization.Types;
using StaffWork.Server.GraphQL.Ware.Types;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;
using StaffWork.Server.Utils.Intarfaces;
using System.ComponentModel.DataAnnotations;

namespace StaffWork.Server.Providers
{
    public class WareProvider : IWareProvider
    {
        private IWareDataProvider wareDataProvider;
        private IFavoriteDataProvider favoriteDataProvider;
        private IAuthorizationProvider authorizationProvider;
        private IHttpContextAccessor httpContextAccessor;
        private IJwtUtils jwtUtils;
        private IMapper mapper;
        private IHashHelper hashHelper;

        public WareProvider(IWareDataProvider wareDataProvider,IFavoriteDataProvider favoriteDataProvider, IHttpContextAccessor httpContextAccessor, IAuthorizationProvider authorizationProvider, IJwtUtils jwtUtils, IMapper mapper, IHashHelper hashHelper)
        {
            this.wareDataProvider = wareDataProvider;
            this.jwtUtils = jwtUtils;
            this.mapper = mapper;
            this.hashHelper = hashHelper;
            this.favoriteDataProvider = favoriteDataProvider;
            this.authorizationProvider = authorizationProvider;
            this.httpContextAccessor = httpContextAccessor;
        }
        public WareModel GetWareById(int id)
        {
            return wareDataProvider.GetWareById(id);
        }
        public List<WareModel> GetAllWares(QuerySettings settings)
        {
            return wareDataProvider.GetAllWares(settings);
        }

        public CRUDWareResponse CreateWare(NewWareModel ware)
        {
            var response = new CRUDWareResponse();

            ICollection<ValidationResult>? validationResults = null;
            if (!Validate(ware, out validationResults))
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    response.Errors.Add(validationResult.ToString());
                }
                response.StatusCode = 401;
                return response;
            }

            response.StatusCode = 201;

            response.Ware = wareDataProvider.CreateWare(ware);

            return response;
        }
        static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }

        CRUDWareResponse IWareProvider.AddWareToFavorite(int wareId)
        {
            var response = new CRUDWareResponse();
            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.Authorized);
            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            response.Ware = wareDataProvider.GetWareById(favoriteDataProvider.AddToFavorite(new NewFavoriteModel
            {
                WareId = wareId,
                UserId = authorizationResponse.User.Id,
            }).Id);
            response.StatusCode = 200;
            return response;
        }

        CRUDWareResponse IWareProvider.RemoveWareFromFavorite(int wareId)
        {
            var response = new CRUDWareResponse();
            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.Authorized);
            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            favoriteDataProvider.RemoveToFavorite(authorizationResponse.User.Id,wareId);
            response.StatusCode = 200;
            return response;
        }

        GetFavoriteWaresResponse IWareProvider.GetUserFavoriteWares()
        {
            var response = new GetFavoriteWaresResponse();
            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.Authorized);
            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            response.Wares = favoriteDataProvider.GetUserFavorite(authorizationResponse.User.Id);

            return response;
        }
    }
}
