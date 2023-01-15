using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Brands
{
    public class BrandType : ObjectGraphType<BrandModel>
    {
        public BrandType()
        {

            Field<NonNullGraphType<IntGraphType>, int>()
               .Name("Id")
               .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Name")
               .Resolve(ctx => ctx.Source.Name);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("CountryManufactured")
               .Resolve(ctx => ctx.Source.CountryManufactured);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Phone")
               .Resolve(ctx => ctx.Source.Phone);
        }
    }
}
