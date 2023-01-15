using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Basket
{
    public class CRUDBasketResponseType : ObjectGraphType<CRUDBasketResponse>
    {
        public CRUDBasketResponseType()
        {
            Field<ListGraphType<StringGraphType>, List<string>?>()
               .Name("Errors")
               .Resolve(ctx => ctx.Source.Errors);

            Field<IntGraphType, int>()
               .Name("StatusCode")
               .Resolve(ctx => ctx.Source.StatusCode);

            Field<BasketWareGraphType, BasketWareGraph?>()
               .Name("Ware")
               .Resolve(ctx => ctx.Source.Ware);
        }
    }
}
