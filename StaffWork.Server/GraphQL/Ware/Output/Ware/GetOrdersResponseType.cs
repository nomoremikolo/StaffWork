using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class GetOrdersResponseType : ObjectGraphType<GetOrdersResponse>
    {
        public GetOrdersResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
              .Name("StatusCode")
              .Resolve(ctx => ctx.Source.StatusCode);

            Field<ListGraphType<OrderGraphType>, List<OrderGraph>>()
               .Name("Wares")
               .Resolve(ctx => ctx.Source.Wares);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);
        }
    }
}
