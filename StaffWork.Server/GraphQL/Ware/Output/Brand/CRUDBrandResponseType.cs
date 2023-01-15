using BusinessLogic.Models;
using GraphQL.Types;
using StaffWork.Server.GraphQL.Ware.Output.Brands;

namespace StaffWork.Server.GraphQL.Ware.Output.Brand
{
    public class CRUDBrandResponseType : ObjectGraphType<CRUDBrandResponse>
    {
        public CRUDBrandResponseType()
        {
            Field<ListGraphType<StringGraphType>, List<string>?>()
               .Name("Errors")
               .Resolve(ctx => ctx.Source.Errors);

            Field<IntGraphType, int>()
               .Name("StatusCode")
               .Resolve(ctx => ctx.Source.StatusCode);

            Field<BrandType, BrandModel?>()
               .Name("Brand")
               .Resolve(ctx => ctx.Source.Brand);
        }
    }
}
