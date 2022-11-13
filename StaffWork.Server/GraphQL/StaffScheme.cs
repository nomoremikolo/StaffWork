using GraphQL.Types;

namespace StaffWork.Server.GraphQL
{
    public class StaffScheme : Schema
    {
        public StaffScheme(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
            Mutation = provider.GetRequiredService<RootMutations>();
        }
    }
}
