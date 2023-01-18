using AutoMapper;
using BusinessLogic;
using BusinessLogic.Models;
using StaffWork.Server.GraphQL.Ware.Output.Basket;
using StaffWork.Server.GraphQL.Ware.Output.Ware;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;
using StaffWork.Server.Utils.Intarfaces;

namespace StaffWork.Server.Providers
{
    public class BasketProvider : IBasketProvider
    {
        private readonly IBasketDataProvider basketDataProvider;
        private IAuthorizationProvider authorizationProvider;
        private IHttpContextAccessor httpContextAccessor;
        public BasketProvider(IBasketDataProvider basketDataProvider, IHttpContextAccessor httpContextAccessor, IAuthorizationProvider authorizationProvider)
        {
            this.basketDataProvider = basketDataProvider;
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
        public CRUDOrderResponse UpdateOrder(int id, string? status, bool? isConfirmed)
        {
            var response = new CRUDOrderResponse();
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
                response.Order = basketDataProvider.UpdateOrder(id, status, isConfirmed);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception)
            {
                response.Errors.Add("Db error");
                response.StatusCode = 500;
                return response;
            }
            //return basketDataProvider.UpdateOrder(id, status, isConfirmed);

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

        public CRUDBasketResponse ClearBasket()
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
                response.Ware = basketDataProvider.ClearBasket(authorizationResponse.User.Id);
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

        public List<OrderGraph> GetOrders(bool? confirmedFilter)
        {
            return basketDataProvider.GetOrders(confirmedFilter);
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
