using GraphQL.Types;

namespace StaffWork.Server.GraphQL.User.Output
{
    public class GetPermissionsListType : ObjectGraphType<GetPermissionsList>
    {
        public GetPermissionsListType()
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
