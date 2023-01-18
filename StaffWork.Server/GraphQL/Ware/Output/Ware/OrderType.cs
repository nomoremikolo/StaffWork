using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class OrderType : ObjectGraphType<OrderModel>
    {
        public OrderType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
               .Name("Id")
               .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Status")
               .Resolve(ctx => ctx.Source.Status);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
               .Name("IsConfirmed")
               .Resolve(ctx => ctx.Source.IsConfirmed);

            Field<NonNullGraphType<IntGraphType>, int>()
               .Name("UserId")
               .Resolve(ctx => ctx.Source.UserId);
        }
    }
}
