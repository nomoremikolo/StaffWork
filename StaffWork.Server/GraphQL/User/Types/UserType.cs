using BusinessLogic.Models;
using GraphQL.Types;
using StaffWork.Server.Utils;

namespace StaffWork.Server.GraphQL.User.Types
{
    public class UserType : ObjectGraphType<UserModel>
    {
        public UserType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Username")
                .Resolve(ctx => ctx.Source.Username);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Name")
               .Resolve(ctx => ctx.Source.Name);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Surname")
               .Resolve(ctx => ctx.Source.Surname);

            Field<NonNullGraphType<BooleanGraphType>, bool>()
               .Name("IsActivated")
               .Resolve(ctx => ctx.Source.IsActivated);

            Field<IntGraphType, int?>()
               .Name("Age")
               .Resolve(ctx => ctx.Source.Age);

            Field<StringGraphType, string?>()
               .Name("Adress")
               .Resolve(ctx => ctx.Source.Adress);

            Field<StringGraphType, string?>()
               .Name("Email")
               .Resolve(ctx => ctx.Source.Email);

            Field<NonNullGraphType<StringGraphType>, string>()
               .Name("Role")
               .Resolve(ctx => ctx.Source.Role);

            Field<NonNullGraphType<ListGraphType<IntGraphType>>, List<int>>()
               .Name("Permissions")
               .Resolve(ctx => StringToArrayConverter.StringToArrayOfNumbers(ctx.Source.Permissions));
        }

    }
}
