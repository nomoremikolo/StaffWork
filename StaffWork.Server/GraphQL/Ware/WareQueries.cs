using AutoMapper;
using BusinessLogic.Models;
using GraphQL;
using GraphQL.Types;
using StaffWork.Server.GraphQL.Ware.Inputs;
using StaffWork.Server.GraphQL.Ware.Output.Basket;
using StaffWork.Server.GraphQL.Ware.Output.Brands;
using StaffWork.Server.GraphQL.Ware.Output.Category;
using StaffWork.Server.GraphQL.Ware.Output.Favorite;
using StaffWork.Server.GraphQL.Ware.Output.Ware;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;

namespace StaffWork.Server.GraphQL.Ware
{
    public class WareQueries : ObjectGraphType
    {
        public WareQueries(IMapper mapper,IBasketProvider basketProvider, IBrandProvider brandProvider, ICategoryProvider categoryProvider,  ICookiesHelper cookiesHelper, IAuthorizationProvider authorizationProvider,  IWareProvider wareProvider, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<GetWaresResponseType>, GetWaresResponse>()
                .Name("GetAllWares")
                .Argument<QuerySettingsInputType>("settings", "Query settings")
                .Resolve(context =>
                {
                    var response = new GetWaresResponse();

                    try
                    {
                        var settings = context.GetArgument<QuerySettings>("settings");
                        if(settings == null)
                        {
                            settings = new QuerySettings();
                        }
                        response.Wares = wareProvider.GetAllWares(settings);
                    }
                    catch (Exception)
                    {
                        response.StatusCode = 500;
                        response.Errors.Add($"Database error");
                        return response;
                    }
                    response.StatusCode = 200;
                    return response;
                }
                );

            Field<NonNullGraphType<GetOrdersResponseType>, GetOrdersResponse>()
                .Name("GetOrders")
                .Argument<BooleanGraphType>("confirmed")
                .Argument<StringGraphType>("OrderNumber")
                .Resolve(context =>
                {
                    var response = new GetOrdersResponse();

                    try
                    {
                        var confirmed = context.GetArgument<bool?>("confirmed");
                        var OrderNumber = context.GetArgument<string?>("OrderNumber");
                        response.Wares = basketProvider.GetOrders(confirmed, OrderNumber);
                    }
                    catch (Exception)
                    {
                        response.StatusCode = 500;
                        response.Errors.Add($"Database error");
                        return response;
                    }
                    response.StatusCode = 200;
                    return response;
                }
                );

            Field<NonNullGraphType<GetCategoriesResponseType>, GetCategoriesResponse>()
                .Name("GetAllCategories")
                .Resolve(context =>
                {
                    var response = new GetCategoriesResponse();

                    try
                    {
                        response = categoryProvider.GetAllCategories();
                    }
                    catch (Exception)
                    {
                        response.StatusCode = 500;
                        response.Errors.Add($"Database error");
                        return response;
                    }
                    response.StatusCode = 200;
                    return response;
                }
                );
            Field<NonNullGraphType<GetBrandsResponseType>, GetBrandsResponse>()
                .Name("GetAllBrands")
                .Resolve(context =>
                {
                    var response = new GetBrandsResponse();

                    try
                    {
                        response = brandProvider.GetAllBrands();
                    }
                    catch (Exception)
                    {
                        response.StatusCode = 500;
                        response.Errors.Add($"Database error");
                        return response;
                    }
                    response.StatusCode = 200;
                    return response;
                }
                );

            Field<NonNullGraphType<GetAuthorizedUserWaresResponseType>, GetAuthorizedUserWaresResponse>()
                 .Name("GetAllWaresAuthorized")
                 .Argument<QuerySettingsInputType>("settings", "Query settings")
                 .Resolve(context =>
                 {
                     var response = new GetAuthorizedUserWaresResponse();

                     try
                     {
                         var settings = context.GetArgument<QuerySettings>("settings");
                         if (settings == null)
                         {
                             settings = new QuerySettings();
                         }
                         response = wareProvider.GetAllWaresWithFavorite(settings);
                     }
                     catch (Exception)
                     {
                         response.StatusCode = 500;
                         response.Errors.Add($"Database error");
                         return response;
                     }
                     response.StatusCode = 200;
                     return response;
                 }
                 );

            Field<NonNullGraphType<GetWareByIdResponseType>, GetWareByIdResponse>()
                .Name("GetWareById")
                .Argument<IntGraphType>("WareId", "Ware id")
                .Resolve(context =>
                {
                    var response = new GetWareByIdResponse();
                    var categoryId = context.GetArgument<int>("WareId");
                    try
                    {
                        response.Ware = wareProvider.GetWareById(categoryId);
                    }
                    catch (Exception)
                    {
                        response.StatusCode = 500;
                        response.Errors.Add($"Database error");
                        return response;
                    }
                    response.StatusCode = 200;
                    return response;
                }
                );

            Field<NonNullGraphType<GetFavoriteWaresResponseType>, GetFavoriteWaresResponse>()
                .Name("GetFavoriteWares")
                .Resolve(context =>
                {
                    var response = new GetFavoriteWaresResponse();

                    var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

                    if (authorizationResponse.StatusCode != 200)
                    {
                        response.StatusCode = authorizationResponse.StatusCode;
                        response.Errors = authorizationResponse.Errors;
                        return response;
                    }
                    try
                    {
                        response = wareProvider.GetUserFavoriteWares();
                    }
                    catch (Exception)
                    {
                        response.StatusCode = 500;
                        response.Errors.Add($"Database error");
                        return response;
                    }
                    response.StatusCode = 200;
                    return response;
                }
                );
            Field<NonNullGraphType<GetBasketWaresResponseType>, GetBasketWaresResponse>()
                .Name("GetWaresFromBasket")
                .Resolve(context =>
                {
                    var response = new GetBasketWaresResponse();

                    response = basketProvider.GetAllBasketWares();

                    return response;
                }
                );

        }
    }
}
