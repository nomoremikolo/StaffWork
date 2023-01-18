using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class OrderGraphType : ObjectGraphType<OrderGraph>
    {
        public OrderGraphType()
        {


            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("OrderId")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
                .Name("IsConfirmed")
                .Resolve(ctx => ctx.Source.IsConfirmed);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Status")
                .Resolve(ctx => ctx.Source.Status);

            Field<NonNullGraphType<ListGraphType<OrderGraphModelType>>, List<OrderGraphModel>>()
                .Name("OrderWares")
                .Resolve(ctx => ctx.Source.OrderWares);


        }
    }
}
