namespace Riwexoyd.TelegramBotEngine.Polling.Contracts
{
    internal interface IUpdateCounter
    {
        bool HasUpdates { get; }

        void ReceiveUpdate();
    }
}
