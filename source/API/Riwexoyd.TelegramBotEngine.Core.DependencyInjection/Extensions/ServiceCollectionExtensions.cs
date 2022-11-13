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
            // Register Bot configuration
            services.Configure<BotConfiguration>(
                configuration.GetSection(botConfigurationSection));

            // Register named HttpClient to benefits from IHttpClientFactory
            // and consume it with ITelegramBotClient typed client.
            // More read:
            //  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
            //  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            services.AddHttpClient("telegram_bot_client")
                    .AddTypedClient<ITelegramBotClient>((httpClient, serviceProvider) =>
                    {
                        IOptions<BotConfiguration> botOptions = serviceProvider.GetRequiredService<IOptions<BotConfiguration>>();
                        TelegramBotClientOptions options = new(botOptions.Value.Token);

                        return new TelegramBotClient(options, httpClient);
                    });

            return services;
        }
    }
}
