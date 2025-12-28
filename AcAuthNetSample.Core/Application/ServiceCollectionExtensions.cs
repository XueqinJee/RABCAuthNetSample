using AcAuthNetSample.Core.Application.Auth;
using AcAuthNetSample.Core.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AcAuthNetSample.Core.Application {
    public static class ServiceCollectionExtensions {

        public static IServiceCollection AddApplication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddCore(configuration);

            // 注册Auth相关服务
            service.AddAuth(configuration);
            return service;
        }

        private static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration) {

            var connectionStrng = configuration.GetConnectionString("SqlServer");
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(connectionStrng);
            });

            services.AddMemoryCache();

            return services;
        }
    }
}
