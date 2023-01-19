using AutoMapper;
using BusinessLogic;
using BusinessLogic.Enums;
using GraphQL;
using GraphQL.Types;
using StaffWork.Server.GraphQL.User.Output;
using StaffWork.Server.GraphQL.User.Types;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;

namespace StaffWork.Server.GraphQL.User
{
    public class UserQueries : ObjectGraphType
    {
        public UserQueries(IMapper mapper, IUserProvider userProvider, ICookiesHelper cookiesHelper, IHttpContextAccessor httpContextAccessor, IAuthorizationProvider authorizationProvider)
        {

            Field<NonNullGraphType<GetUsersResponseType>, GetUsersResponse>()
                .Name("GetAll") 
                .Argument<StringGraphType>("KeyWords")
                .Resolve(context =>
                {
                    var response = new GetUsersResponse();

                    var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.GetUsers);

                    if (authorizationResponse.StatusCode != 200)
                    {
                        response.StatusCode = authorizationResponse.StatusCode;
                        response.Errors = authorizationResponse.Errors;
                        return response;
                    }

                    try
                    {
                        var keyWords = context.GetArgument<string?>("KeyWords");
                        response.Users = userProvider.GetUsers(keyWords);
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


            Field<NonNullGraphType<CRUDUserResponseType>, CRUDUserResponse>()
                .Name("GetUserById")
                .Argument<IntGraphType>("UserId")
                .Resolve(context =>
                {
                    var response = new CRUDUserResponse();

                    var userId = context.GetArgument<int>("UserId");
                    response = userProvider.GetUserById(userId);

                    return response;
                }
                );

            Field<NonNullGraphType<GetPermissionsQueryResponseType>, GetPermissionsQueryResponse>()
                .Name("GetAllPermissions")
                .Resolve(ctx =>
                {
                    var response = new GetPermissionsQueryResponse();
                    var authorizationResponse = authorizationProvider.AuthorizeUser(httpContextAccessor.HttpContext, AuthorizationPolicies.Authorized);
                    if (authorizationResponse.StatusCode != 200)
                    {
                        response.StatusCode = authorizationResponse.StatusCode;
                        response.Errors = authorizationResponse.Errors;
                        return response;
                    }

                    try
                    {
                        response.Permissions = authorizationProvider.GetAllPermissions();
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
