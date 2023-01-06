using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Types
{
    public class GetFavoriteWaresResponseType : ObjectGraphType<GetFavoriteWaresResponse>
    {
        public GetFavoriteWaresResponseType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("StatusCode")
                .Resolve(ctx => ctx.Source.StatusCode);

            Field<ListGraphType<FavoriteWareType>, List<FavoriteWareModel>>()
               .Name("Wares")
               .Resolve(ctx => ctx.Source.Wares);

            Field<ListGraphType<StringGraphType>, List<string>?>()
              .Name("Errors")
              .Resolve(ctx => ctx.Source.Errors);
        }
    }
}
