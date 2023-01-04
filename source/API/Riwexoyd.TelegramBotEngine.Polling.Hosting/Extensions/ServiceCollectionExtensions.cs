using Microsoft.Extensions.DependencyInjection;

using Riwexoyd.TelegramBotEngine.Polling.Extensions;
using Riwexoyd.TelegramBotEngine.Polling.Hosting.Services;

namespace Riwexoyd.TelegramBotEngine.Polling.Hosting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotPolling(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.RegisterTelegramBotPollingServices();
            services.AddHostedService<PollingBackgroundService>();

            return services;
        }
    }
}
