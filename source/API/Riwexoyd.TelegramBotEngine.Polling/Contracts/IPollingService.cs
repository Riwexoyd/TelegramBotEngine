namespace Riwexoyd.TelegramBotEngine.Polling.Contracts
{
    public interface IPollingService
    {
        Task PollAsync(CancellationToken cancellationToken);
    }
}
