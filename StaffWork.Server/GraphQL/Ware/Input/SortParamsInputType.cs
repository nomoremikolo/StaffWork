using BusinessLogic.Models;
using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Inputs
{
    public class SortParamsInputType : InputObjectGraphType<SortParamsModel>
    {
        public SortParamsInputType()
        {
            Field<StringGraphType, string>()
              .Name("Value")
              .Resolve(ctx => ctx.Source.Value);

            Field<BooleanGraphType, bool>()
              .Name("IsReverse")
              .Resolve(ctx => ctx.Source.IsReverse);
        }
    }
}
