using GraphQL.Types;
using StaffWork.Server.GraphQL.Authorization;
using StaffWork.Server.GraphQL.User;

namespace StaffWork.Server.GraphQL
{
    internal class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<UserQueries>()
                .Name("User")
                .Resolve(_ => new { });

            Field<AuthorizationQueries>()
             .Name("Authorization")
             .Resolve(_ => new { });
        }
    }
}