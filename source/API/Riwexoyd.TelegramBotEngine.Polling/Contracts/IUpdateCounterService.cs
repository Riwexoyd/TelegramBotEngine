namespace Riwexoyd.TelegramBotEngine.Polling.Contracts
{
    internal interface IUpdateCounterService
    {
        bool HasUpdates { get; }

        void ReceiveUpdate();

        void Reset();
    }
}
