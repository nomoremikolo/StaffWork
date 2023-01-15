using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Ware.Input
{
    public class CreateBrandInputType : InputObjectGraphType<CreateBrandInput>
    {
        public CreateBrandInputType()
        {
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
