using GraphQL.Types;

namespace StaffWork.Server.GraphQL.Authorization.Inputs
{
    public class UserLoginInputType : InputObjectGraphType<UserLoginInput>
    {
        public UserLoginInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                    .Name("Username")
                    .Resolve(ctx => ctx.Source.Username);

            Field<NonNullGraphType<StringGraphType>, string>()
                   .Name("Password")
                   .Resolve(ctx => ctx.Source.Password);
        }
    }
}
