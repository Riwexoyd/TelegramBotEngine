using Microsoft.Extensions.DependencyInjection;
using Riwexoyd.TelegramBotEngine.Polling.Contracts;
using Riwexoyd.TelegramBotEngine.Polling.Services;
using Telegram.Bot.Polling;

namespace Riwexoyd.TelegramBotEngine.Polling.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterTelegramBotPollingServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddScoped<IUpdateReceiverService, UpdateReceiverService>();
            services.AddScoped<IUpdateHandler, PollingUpdateHandler>();
            services.AddScoped<IUpdateCounter, UpdateCounter>();
            services.AddSingleton<IPollingTimeoutService, PollingTimeoutService>();
            services.AddSingleton<IPollingService, PollingService>();

            return services;
        }
    }
}
