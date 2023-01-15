using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Favorite
{
    public class FavoriteWareType : ObjectGraphType<FavoriteWareModel>
    {
        public FavoriteWareType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
               .Name("FavoriteId")
               .Resolve(ctx => ctx.Source.FavoriteId);

            Field<NonNullGraphType<IntGraphType>, int>()
               .Name("WareId")
               .Resolve(ctx => ctx.Source.Id);

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
        }
    }
}
