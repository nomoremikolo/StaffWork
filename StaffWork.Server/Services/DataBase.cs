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
            services.AddTransient<IWareDataProvider>(provider =>
            {
                return new WareDataProvider(configuration["ConnectionStrings:default"]);
            });
            services.AddTransient<IFavoriteDataProvider>(provider =>
            {
                return new FavoriteDataProvider(configuration["ConnectionStrings:default"]);
            });
            return services;
        }
    }
}
