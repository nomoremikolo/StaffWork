using GraphQL.Types;

namespace StaffWork.Server.GraphQL.User.Input
{
    public class UpdateUserByAdminInputType : InputObjectGraphType<UpdateUserByAdminInput>
    {
        public UpdateUserByAdminInputType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Name")
                .Resolve(ctx => ctx.Source.Name);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Surname")
                .Resolve(ctx => ctx.Source.Surname);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Username")
                .Resolve(ctx => ctx.Source.Username);

            Field<IntGraphType, int?>()
                .Name("Age")
                .Resolve(ctx => ctx.Source.Age);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
                .Name("IsActivated")
                .Resolve(ctx => ctx.Source.IsActivated);

            Field<StringGraphType, string?>()
                .Name("Email")
                .Resolve(ctx => ctx.Source.Email);

            Field<StringGraphType, string?>()
                .Name("Role")
                .Resolve(ctx => ctx.Source.Role);

            Field<StringGraphType, string?>()
                .Name("Adress")
                .Resolve(ctx => ctx.Source.Adress);
        }
    }
}
