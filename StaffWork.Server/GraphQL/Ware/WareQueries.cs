using AutoMapper;
using BusinessLogic.Models;
using GraphQL;
using GraphQL.Types;
using StaffWork.Server.GraphQL.Ware.Inputs;
using StaffWork.Server.GraphQL.Ware.Types;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;

namespace StaffWork.Server.GraphQL.Ware
{
    public class WareQueries : ObjectGraphType
    {
        public WareQueries(IMapper mapper, ICookiesHelper cookiesHelper, IAuthorizationProvider authorizationProvider,  IWareProvider wareProvider, IHttpContextAccessor httpContextAccessor)
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

            Field<NonNullGraphType<CRUDWareResponseType>, CRUDWareResponse>()
                .Name("GetWareById")
                .Argument<IntGraphType>("WareId", "Ware id")
                .Resolve(context =>
                {
                    var response = new CRUDWareResponse();
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
                        var settings = context.GetArgument<QuerySettings>("settings");
                        if (settings == null)
                        {
                            settings = new QuerySettings();
                        }
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
        }
    }
}
