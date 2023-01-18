using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class CRUDOrderResponseType : ObjectGraphType<CRUDOrderResponse>
    {
        public CRUDOrderResponseType()
        {
            Field<ListGraphType<StringGraphType>, List<string>?>()
               .Name("Errors")
               .Resolve(ctx => ctx.Source.Errors);

            Field<IntGraphType, int>()
               .Name("StatusCode")
               .Resolve(ctx => ctx.Source.StatusCode);

            Field<OrderType, OrderModel?>()
               .Name("Order")
               .Resolve(ctx => ctx.Source.Order);
        }
    }
}
