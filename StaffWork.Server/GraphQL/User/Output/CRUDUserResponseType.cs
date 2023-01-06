using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.User.Types
{
    public class CRUDUserResponseType : ObjectGraphType<CRUDUserResponse>
    {
        public CRUDUserResponseType()
        {
            Field<ListGraphType<StringGraphType>, List<string>?>()
               .Name("Errors")
               .Resolve(ctx => ctx.Source.Errors);

            Field<IntGraphType, int>()
               .Name("StatusCode")
               .Resolve(ctx => ctx.Source.StatusCode);

            Field<UserType, UserModel?>()
               .Name("User")
               .Resolve(ctx => ctx.Source.User);


            Field<StringGraphType, string?>()
               .Name("Token")
               .Resolve(ctx => ctx.Source.JwtToken);

            Field<StringGraphType, string?>()
               .Name("RefreshToken")
               .Resolve(ctx => ctx.Source.RefreshToken);
        }
    }
}
