using GraphQL.Types;
using StaffWork.Server.JwtAuthorization;

namespace StaffWork.Server.GraphQL.User.Output
{
    public class GetPermissionsQueryResponseType : ObjectGraphType<GetPermissionsQueryResponse>
    {
        public GetPermissionsQueryResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
               .Name("StatusCode")
               .Resolve(ctx => ctx.Source.StatusCode);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);

            Field<ListGraphType<PermissionType>, List<PermissionModel>?>()
              .Name("Permissions")
              .Resolve(ctx => ctx.Source.Permissions);
        }
    }
}
