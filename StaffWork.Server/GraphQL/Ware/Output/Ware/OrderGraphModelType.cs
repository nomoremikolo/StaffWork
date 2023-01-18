using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class OrderGraphModelType : ObjectGraphType<OrderGraphModel>
    {
        public OrderGraphModelType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("WareName")
                .Resolve(ctx => ctx.Source.WareName);
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("BrandName")
                .Resolve(ctx => ctx.Source.BrandName);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("CategoryName")
                .Resolve(ctx => ctx.Source.CategoryName);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Description")
                .Resolve(ctx => ctx.Source.Description);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Sizes")
                .Resolve(ctx => ctx.Source.Sizes);

            Field<NonNullGraphType<DecimalGraphType>, decimal>()
                .Name("Price")
                .Resolve(ctx => ctx.Source.Price);

            Field<NonNullGraphType<DecimalGraphType>, decimal>()
                .Name("OldPrice")
                .Resolve(ctx => ctx.Source.OldPrice);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
                .Name("IsDiscount")
                .Resolve(ctx => ctx.Source.IsDiscount);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("CountInStorage")
                .Resolve(ctx => ctx.Source.CountInStorage);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("WareId")
                .Resolve(ctx => ctx.Source.WareId);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Count")
                .Resolve(ctx => ctx.Source.Count);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Size")
                .Resolve(ctx => ctx.Source.Size);
        }
    }
}
