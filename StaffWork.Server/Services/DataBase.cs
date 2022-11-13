using BusinessLogic;
using MSSQLProvider;

namespace StaffWork.Server.Services
{
    public static class DataBase
    {
        public static IServiceCollection AddDBProviders(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserDataProvider>(provider =>
            {
                return new UserDataProvider(configuration["ConnectionStrings:default"]);
            });

            return services;
        }
    }
}
