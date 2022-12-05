using AutoMapper;
using GraphQL;
using GraphQL.Types;
using StaffWork.Server.GraphQL.Authorization.Types;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Providers.Interfaces;

namespace StaffWork.Server.GraphQL.Authorization
{
    public class AuthorizationMutation : ObjectGraphType
    {
        public AuthorizationMutation(IMapper mapper, ICookiesHelper cookiesHelper, IAuthorizationProvider authorizationProvider, IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<SignInQueryResponseType>, SignInQueryResponse>()
                .Name("RefreshToken")
                .Argument<StringGraphType, string>("RefreshToken", "")
                .Resolve(ctx =>
                {
                    var refreshToken = ctx.GetArgument<string>("RefreshToken");
                    if (refreshToken == null)
                    {
                        refreshToken = cookiesHelper.GetTokenCookie(httpContextAccessor.HttpContext);
                    }
                    var authenticationResponse = authorizationProvider.LoginByRefreshToken(refreshToken);

                    var queryResponse = mapper.Map<SignInQueryResponse>(authenticationResponse);
                    if (queryResponse.StatusCode == 200)
                    {
                        cookiesHelper.SetTokenCookie(authenticationResponse.RefreshToken, httpContextAccessor.HttpContext);
                    }

                    return queryResponse;
                }
                );
        }
    }
}
