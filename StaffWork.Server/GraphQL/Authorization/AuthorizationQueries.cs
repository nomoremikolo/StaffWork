using AutoMapper;
using BusinessLogic.Models;
using GraphQL;
using GraphQL.Types;
using StaffWork.Server.GraphQL.Authorization.Inputs;
using StaffWork.Server.GraphQL.Authorization.Types;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;

namespace StaffWork.Server.GraphQL.Authorization
{
    public class AuthorizationQueries : ObjectGraphType
    {
        public AuthorizationQueries(IMapper mapper, ICookiesHelper cookiesHelper, IAuthorizationProvider authorizationProvider, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<SignInQueryResponseType>, SignInQueryResponse>()
                .Name("Login")
                .Argument<UserLoginInputType>("userLogin", "username & password")
                .Resolve(context =>
                {
                    var userInput = context.GetArgument<UserLoginInput>("userLogin");
                    var userLoginModel = mapper.Map<UserLoginModel>(userInput);
                    var authenticationResponse = authorizationProvider.Login(userLoginModel);

                    var response = mapper.Map<SignInQueryResponse>(authenticationResponse);
                    if (response.StatusCode == 200)
                    {
                        cookiesHelper.SetTokenCookie(authenticationResponse.RefreshToken, httpContextAccessor.HttpContext);
                    }
                    return response;
                }
                );
        }
    }
}
