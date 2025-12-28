using AcAuthNetSample.Core.Application.Configuration.Options;
using AcAuthNetSample.Core.Application.Sms.Interfaces;
using AcAuthNetSample.Core.Application.Sms.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AcAuthNetSample.Core.Application.Sms {
    internal static class SmsServiceExtensions {

        public static IServiceCollection AddSmsService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailNotifyService, EmailNotifyService>();

            services.AddOptions<EmailSettingOptions>()
                .Bind(configuration.GetSection(EmailSettingOptions.Name))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }
    }
}
