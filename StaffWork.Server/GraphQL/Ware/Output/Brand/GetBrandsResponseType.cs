using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Brands
{
    public class GetBrandsResponseType : ObjectGraphType<GetBrandsResponse>
    {
        public GetBrandsResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
              .Name("StatusCode")
              .Resolve(ctx => ctx.Source.StatusCode);

            Field<ListGraphType<BrandType>, List<BrandModel>>()
               .Name("Brands")
               .Resolve(ctx => ctx.Source.Brands);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);
        }
    }
}
