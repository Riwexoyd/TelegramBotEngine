using Microsoft.Extensions.Logging;
using Riwexoyd.TelegramBotEngine.Core.Contracts;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Riwexoyd.TelegramBotEngine.Core.Services
{
    public sealed class PollingUpdateHandler : IUpdateHandler
    {
        private readonly ILogger<PollingUpdateHandler> _logger;
        private readonly ITelegramUpdateHandler _telegramUpdateHandler;

        public PollingUpdateHandler(ILogger<PollingUpdateHandler> logger, ITelegramUpdateHandler telegramUpdateHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _telegramUpdateHandler = telegramUpdateHandler ?? throw new ArgumentNullException(nameof(telegramUpdateHandler));
        }

        public Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
        {
            return _telegramUpdateHandler.HandleAsync(update, cancellationToken);
        }

        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            string errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogError(exception, "HandlePollingErrorAsync: {ErrorMessage}", errorMessage);

            // Cooldown in case of network connection error
            if (exception is RequestException)
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
    }
}