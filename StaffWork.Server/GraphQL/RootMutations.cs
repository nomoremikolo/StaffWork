using GraphQL.Types;
using StaffWork.Server.GraphQL.Authorization;
using StaffWork.Server.GraphQL.User;

namespace StaffWork.Server.GraphQL
{
    public class RootMutations : ObjectGraphType
    {
        public RootMutations()
        {
            Field<UserMutations>()
               .Name("User")
               .Resolve(_ => new { });
            Field<AuthorizationMutation>()
             .Name("Authorization")
             .Resolve(_ => new { });
        }
    }
}