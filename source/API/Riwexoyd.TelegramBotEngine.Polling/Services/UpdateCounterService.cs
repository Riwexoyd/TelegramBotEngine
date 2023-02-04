using Riwexoyd.TelegramBotEngine.Polling.Contracts;

namespace Riwexoyd.TelegramBotEngine.Polling.Services
{
    internal sealed class UpdateCounterService : IUpdateCounterService
    {
        public bool HasUpdates { get; private set; }

        public void ReceiveUpdate()
        {
            HasUpdates = true;
        }

        public void Reset()
        {
            HasUpdates = false;
        }
    }
}