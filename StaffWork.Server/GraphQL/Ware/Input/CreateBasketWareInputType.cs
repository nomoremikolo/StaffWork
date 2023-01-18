using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Input
{
    public class CreateBasketWareInputType : InputObjectGraphType<CreateBasketWareInput>
    {
        public CreateBasketWareInputType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("WareId")
                .Resolve(ctx => ctx.Source.WareId);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Count")
                .Resolve(ctx => ctx.Source.Count);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Size")
                .Resolve(ctx => ctx.Source.Size);
        }
    }
}
