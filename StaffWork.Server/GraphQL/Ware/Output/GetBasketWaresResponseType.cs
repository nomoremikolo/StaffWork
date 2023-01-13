using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output
{
    public class GetBasketWaresResponseType : ObjectGraphType<GetBasketWaresResponse>
    {
        public GetBasketWaresResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("StatusCode")
                .Resolve(ctx => ctx.Source.StatusCode);

            Field<ListGraphType<BasketWareGraphType>, List<BasketWareGraph>>()
               .Name("Wares")
               .Resolve(ctx => ctx.Source.Wares);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);
        }
    }
}
