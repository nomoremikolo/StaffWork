using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output
{
    public class GetWaresResponseType : ObjectGraphType<GetWaresResponse>
    {
        public GetWaresResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
              .Name("StatusCode")
              .Resolve(ctx => ctx.Source.StatusCode);

            Field<ListGraphType<WareType>, List<WareModel>>()
               .Name("Wares")
               .Resolve(ctx => ctx.Source.Wares);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);
        }
    }
}
