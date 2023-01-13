using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output
{
    public class NewBasketWareType : ObjectGraphType<NewBasketWareModel>
    {
        public NewBasketWareType()
        {
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
