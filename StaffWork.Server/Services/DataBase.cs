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
            services.AddTransient<IBasketDataProvider>(provider =>
            {
                return new BasketDataProvider(configuration["ConnectionStrings:default"]);
            });
            services.AddTransient<ICategoryDataProvider>(provider =>
            {
                return new CategoryDataProvider(configuration["ConnectionStrings:default"]);
            });
            services.AddTransient<IBrandDataProvider>(provider =>
            {
                return new BrandDataProvider(configuration["ConnectionStrings:default"]);
            });
            return services;
        }
    }
}
