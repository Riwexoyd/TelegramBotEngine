using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Riwexoyd.TelegramBotEngine.Core.Configurations;

using Telegram.Bot;

namespace Riwexoyd.TelegramBotEngine.Core.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration, string botConfigurationSection)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            if (string.IsNullOrEmpty(botConfigurationSection))
                throw new ArgumentNullException(nameof(botConfigurationSection));

            services.Configure<TelegramBotConfiguration>(
                configuration.GetSection(botConfigurationSection));

            services.AddHttpClient<ITelegramBotClient, TelegramBotClient>((httpClient, serviceProvider) =>
            {
                IOptions<TelegramBotConfiguration> botOptions = serviceProvider.GetRequiredService<IOptions<TelegramBotConfiguration>>();
                TelegramBotClientOptions options = new(botOptions.Value.Token);

                return new TelegramBotClient(options, httpClient);
            });

            return services;
        }
    }
}
