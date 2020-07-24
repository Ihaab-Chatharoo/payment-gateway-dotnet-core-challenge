using Microsoft.Extensions.DependencyInjection;
using Checkout.Repository.DB;
using Checkout.Repository;
using Checkout.Service;

namespace Checkout.Ioc
{
    public static class IocContainer
    {
        public static void ConfigureIOC(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>(); ;
            services.AddDbContext<GatewayDBContext>();
        }
    }
}
