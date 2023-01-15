using BusinessLogic.Enums;
using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Inputs
{
    public class QuerySettingsInputType : InputObjectGraphType<QuerySettings>
    {
        public QuerySettingsInputType()
        {
            Field<SortParamsInputType, SortParamsModel?>()
                .Name("SortParam")
                .Resolve(ctx => ctx.Source.SortParam);

            Field<IntGraphType, int?>()
                .Name("CategoryId")
                .Resolve(ctx => ctx.Source.CategoryId);

            Field<IntGraphType, int?>()
                .Name("CountOfRecords")
                .Resolve(ctx => ctx.Source.CountOfRecords);

            Field<StringGraphType, FilterEnum?>()
                .Name("Filter")
                .Resolve(ctx => ctx.Source.Filter);

            Field<StringGraphType, string?>()
                .Name("KeyWords")
                .Resolve(ctx => ctx.Source.KeyWords);
        }
    }
}
