using BusinessLogic.Models;
using GraphQL.Types;
using StaffWork.Server.GraphQL.User.Types;

namespace StaffWork.Server.GraphQL.Authorization.Types
{
    public class SignOutQueryResponseType : ObjectGraphType<SignOutQueryResponse>
    {
        public SignOutQueryResponseType()
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

        }
    }
}
