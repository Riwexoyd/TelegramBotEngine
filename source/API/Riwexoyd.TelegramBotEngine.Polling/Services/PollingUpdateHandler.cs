using Microsoft.Extensions.Logging;

using Riwexoyd.TelegramBotEngine.Core.Contracts;
using Riwexoyd.TelegramBotEngine.Polling.Contracts;
using Riwexoyd.TelegramBotEngine.Polling.Exceptions;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Riwexoyd.TelegramBotEngine.Polling.Services
{
    internal sealed class PollingUpdateHandler : IUpdateHandler
    {
        private readonly ITelegramUpdateHandler _telegramUpdateHandler;
        private readonly IUpdateCounterService _updateCounterService;

        public PollingUpdateHandler(ITelegramUpdateHandler telegramUpdateHandler, IUpdateCounterService updateCounterService)
        {
            _telegramUpdateHandler = telegramUpdateHandler ?? throw new ArgumentNullException(nameof(telegramUpdateHandler));
            _updateCounterService = updateCounterService ?? throw new ArgumentNullException(nameof(updateCounterService));
        }

        public Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
        {
            _updateCounterService.ReceiveUpdate();
            return _telegramUpdateHandler.HandleAsync(update, cancellationToken);
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            _updateCounterService.ReceiveUpdate();
            string errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            return Task.FromException(new PollingException(errorMessage, exception));
        }
    }
}