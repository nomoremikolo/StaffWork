using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class GetAuthorizedUserWaresResponseType : ObjectGraphType<GetAuthorizedUserWaresResponse>
    {
        public GetAuthorizedUserWaresResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
              .Name("StatusCode")
              .Resolve(ctx => ctx.Source.StatusCode);

            Field<ListGraphType<AuthorizedWareType>, List<AuthorizedUserWareModel>>()
               .Name("Wares")
               .Resolve(ctx => ctx.Source.Wares);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);
        }
    }
}
