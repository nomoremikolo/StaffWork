using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Category
{
    public class CRUDCategoryResponseType : ObjectGraphType<CRUDCategoryResponse>
    {
        public CRUDCategoryResponseType()
        {
            Field<ListGraphType<StringGraphType>, List<string>?>()
               .Name("Errors")
               .Resolve(ctx => ctx.Source.Errors);

            Field<IntGraphType, int>()
               .Name("StatusCode")
               .Resolve(ctx => ctx.Source.StatusCode);

            Field<CategoryType, CategoryModel?>()
               .Name("Category")
               .Resolve(ctx => ctx.Source.Category);
        }
    }
}
