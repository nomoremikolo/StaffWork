using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Basket
{
    public class BasketWareType : ObjectGraphType<BasketWare>
    {
        public BasketWareType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("WareId")
                .Resolve(ctx => ctx.Source.WareId);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("BasketId")
                .Resolve(ctx => ctx.Source.BasketId);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Count")
                .Resolve(ctx => ctx.Source.Count);
        }
    }
}
