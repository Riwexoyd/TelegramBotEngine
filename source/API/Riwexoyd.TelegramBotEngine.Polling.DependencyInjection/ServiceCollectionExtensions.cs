using Microsoft.Extensions.DependencyInjection;

using Riwexoyd.TelegramBotEngine.Core.Services;
using Riwexoyd.TelegramBotEngine.Polling.Contracts;

using Telegram.Bot.Polling;

namespace Riwexoyd.TelegramBotEngine.Polling.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotPolling(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddScoped<IUpdateReceiverService, UpdateReceiverService>();
            services.AddScoped<IUpdateHandler, PollingUpdateHandler>();
            services.AddHostedService<PollingBackgroundService>();

            return services;
        }
    }
}
