using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.User.Types
{
    public class GetUsersResponseType : ObjectGraphType<GetUsersResponse>
    {
        public GetUsersResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
              .Name("StatusCode")
              .Resolve(ctx => ctx.Source.StatusCode);

            Field<ListGraphType<UserType>, List<UserModel>>()
               .Name("Users")
               .Resolve(ctx => ctx.Source.Users);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);
        }
    }
}
