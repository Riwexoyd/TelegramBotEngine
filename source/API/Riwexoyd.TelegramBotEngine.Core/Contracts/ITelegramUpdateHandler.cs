using Telegram.Bot.Types;

namespace Riwexoyd.TelegramBotEngine.Core.Contracts
{
    public interface ITelegramUpdateHandler
    {
        Task HandleAsync(Update update, CancellationToken cancellationToken);
    }
}
