using AutoMapper;
using BusinessLogic;
using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Ware.Output;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;
using StaffWork.Server.Utils.Intarfaces;

namespace StaffWork.Server.Providers
{
    public class BasketProvider : IBasketProvider
    {
        private readonly IBasketDataProvider basketDataProvider;
        private readonly IJwtUtils jwtUtils;
        private readonly IMapper mapper;
        private readonly IHashHelper hashHelper;
        private IAuthorizationProvider authorizationProvider;
        private IHttpContextAccessor httpContextAccessor;
        public BasketProvider(IBasketDataProvider basketDataProvider, IJwtUtils jwtUtils, IMapper mapper, IHashHelper hashHelper, IHttpContextAccessor httpContextAccessor, IAuthorizationProvider authorizationProvider)
        {
            this.basketDataProvider = basketDataProvider;
            this.jwtUtils = jwtUtils;
            this.mapper = mapper;
            this.hashHelper = hashHelper;
            this.authorizationProvider = authorizationProvider;
            this.httpContextAccessor = httpContextAccessor;
        }

        public CRUDBasketResponse AddToBasket(NewBasketWareModel newBasketWare)
        {
            var response = new CRUDBasketResponse();
            response.StatusCode = 400;

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            newBasketWare.UserId = authorizationResponse.User.Id;
            try
            {
                response.Ware = basketDataProvider.AddToBasket(newBasketWare);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
            }
            
        }

        public CRUDBasketResponse ChangeBasketWareCount(NewBasketWareModel newBasketWare)
        {
            var response = new CRUDBasketResponse();
            response.StatusCode = 400;

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            newBasketWare.UserId = authorizationResponse.User.Id;
            try
            {
                response.Ware = basketDataProvider.ChangeBasketWareCount(newBasketWare);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
            }
        }

        public GetBasketWaresResponse ConfirmOrder()
        {
            var response = new GetBasketWaresResponse();
            response.StatusCode = 400;

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }
            try
            {
                response.Wares = basketDataProvider.ConfirmOrder(authorizationResponse.User.Id);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
            }
        }

        public GetBasketWaresResponse GetAllBasketWares()
        {
            var response = new GetBasketWaresResponse();
            response.StatusCode = 400;

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            try
            {
                response.Wares = basketDataProvider.GetAllWaresFromBasket(authorizationResponse.User.Id);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
            }
        }

        public CRUDBasketResponse GetBasketWareById(int wareId)
        {
            var response = new CRUDBasketResponse();
            response.StatusCode = 400;

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            try
            {
                response.Ware = basketDataProvider.GetWareFromBasketById(wareId, authorizationResponse.User.Id);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
            }
        }

        public CRUDBasketResponse RemoveFromBasket(int basketWareId)
        {
            var response = new CRUDBasketResponse();
            response.StatusCode = 400;

            var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

            if (authorizationResponse.StatusCode != 200)
            {
                response.StatusCode = authorizationResponse.StatusCode;
                response.Errors = authorizationResponse.Errors;
                return response;
            }

            try
            {
                //response.Ware = basketDataProvider.RemoveWareFromBasket(basketWareId, authorizationResponse.User.Id);
                basketDataProvider.RemoveWareFromBasket(basketWareId, authorizationResponse.User.Id);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
            }
        }
    }
}
