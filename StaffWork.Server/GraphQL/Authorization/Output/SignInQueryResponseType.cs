using BusinessLogic.Models;
using GraphQL.Types;
using StaffWork.Server.GraphQL.User.Types;

namespace StaffWork.Server.GraphQL.Authorization.Types
{
    public class SignInQueryResponseType : ObjectGraphType<SignInQueryResponse>
    {
        public SignInQueryResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
               .Name("StatusCode")
               .Resolve(ctx => ctx.Source.StatusCode);

            Field<StringGraphType, string?>()
               .Name("Token")
               .Resolve(ctx => ctx.Source.JwtToken);

            Field<StringGraphType, string?>()
               .Name("RefreshToken")
               .Resolve(ctx => ctx.Source.RefreshToken);

            Field<UserType, UserModel?>()
               .Name("User")
               .Resolve(ctx => ctx.Source.User);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);
        }
    }
}
