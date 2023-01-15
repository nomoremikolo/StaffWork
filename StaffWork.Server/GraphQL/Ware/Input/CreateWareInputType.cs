using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Inputs
{
    public class CreateWareInputType : InputObjectGraphType<CreateWareInput>
    {
        public CreateWareInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(ctx => ctx.Source.Name);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("BrandId")
                .Resolve(ctx => ctx.Source.BrandId);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("CategoryId")
                .Resolve(ctx => ctx.Source.CategoryId);

            Field<StringGraphType, string?>()
                .Name("Description")
                .Resolve(ctx => ctx.Source.Description);

            Field<StringGraphType, string?>()
                .Name("Sizes")
                .Resolve(ctx => ctx.Source.Sizes);

            Field<NonNullGraphType<DecimalGraphType>, decimal>()
                .Name("Price")
                .Resolve(ctx => ctx.Source.Price);

            Field<DecimalGraphType, decimal?>()
                .Name("OldPrice")
                .Resolve(ctx => ctx.Source.OldPrice);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
                .Name("IsDiscount")
                .Resolve(ctx => ctx.Source.IsDiscount);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("CountInStorage")
                .Resolve(ctx => ctx.Source.CountInStorage);

            Field<StringGraphType, string?>()
                .Name("Thumbnail")
                .Resolve(ctx => ctx.Source.Thumbnail);

            Field<StringGraphType, string?>()
                .Name("Images")
                .Resolve(ctx => ctx.Source.Images);
        }
    }
}
