namespace Riwexoyd.TelegramBotEngine.Polling.Contracts
{
    internal interface IPollingTimeoutService
    {
        Task WaitAsync(CancellationToken cancellationToken);
    }
}
