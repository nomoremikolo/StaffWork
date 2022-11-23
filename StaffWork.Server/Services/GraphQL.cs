using GraphQL;
using GraphQL.Server;
using StaffWork.Server.GraphQL;
using StaffWork.Server.JwtAuthorization;
using StaffWork.Server.JwtAuthorization.Interfaces;
using StaffWork.Server.Provider;
using StaffWork.Server.Providers;
using StaffWork.Server.Providers.Interfaces;
using StaffWork.Server.Utils;
using StaffWork.Server.Utils.Intarfaces;

namespace StaffWork.Server.Services
{
    public static class GraphQL
    {
        public static IServiceCollection AddGraphQLAPI(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<StaffScheme>();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.UnhandledExceptionDelegate = (context) =>
                {
                    context.ErrorMessage = context.Exception.Message;
                };
            })
                .AddSystemTextJson()
                .AddGraphTypes(typeof(StaffScheme));

            services.AddSingleton<RootQuery>();
            services.AddSingleton<RootMutations>();
            return services;
        }
        public static IServiceCollection AddGraphQLProviders(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationProvider, AuthorizationProvider>();
            services.AddTransient<IJwtUtils, JwtUtils>();
            services.AddTransient<IUserProvider, UserProvider>();
            //services.AddTransient<ITimerItemProvider, TimerItemProvider>();
            //services.AddTransient<IVacationProvider, VacationsProvider>();
            //services.AddTransient<ICalendarProvider, CalendarProvider>();
            services.AddTransient<ICookiesHelper, CookiesHelper>();
            services.AddTransient<IHashHelper, HashHelper>();
            return services;
        }
    }
}
