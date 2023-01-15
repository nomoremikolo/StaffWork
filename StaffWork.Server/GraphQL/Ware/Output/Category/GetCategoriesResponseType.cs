using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Output.Category
{
    public class GetCategoriesResponseType : ObjectGraphType<GetCategoriesResponse>
    {
        public GetCategoriesResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
              .Name("StatusCode")
              .Resolve(ctx => ctx.Source.StatusCode);

            Field<ListGraphType<CategoryType>, List<CategoryModel>>()
               .Name("Categories")
               .Resolve(ctx => ctx.Source.Categories);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);
        }
    }
}
