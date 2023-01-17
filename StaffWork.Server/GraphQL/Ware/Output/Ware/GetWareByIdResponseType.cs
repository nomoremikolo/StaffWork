using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Ware
{
    public class GetWareByIdResponseType : ObjectGraphType<GetWareByIdResponse>
    {
        public GetWareByIdResponseType()
        {
            Field<ListGraphType<StringGraphType>, List<string>?>()
               .Name("Errors")
               .Resolve(ctx => ctx.Source.Errors);

            Field<IntGraphType, int>()
               .Name("StatusCode")
               .Resolve(ctx => ctx.Source.StatusCode);

            Field<WareModelWithBrandAndCategoryType, WareModelWithBrandAndCategory?>()
               .Name("Ware")
               .Resolve(ctx => ctx.Source.Ware);
        }
    }
}

