using GraphQL.Types;
using StaffWork.Server.JwtAuthorization;

namespace StaffWork.Server.GraphQL.User.Output
{
    public class PermissionType : ObjectGraphType<PermissionModel>
    {
        public PermissionType()
        {

            Field<NonNullGraphType<IntGraphType>, int>()
              .Name("Id")
              .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
              .Name("Name")
              .Resolve(ctx => ctx.Source.Name);
        }
    }
}
