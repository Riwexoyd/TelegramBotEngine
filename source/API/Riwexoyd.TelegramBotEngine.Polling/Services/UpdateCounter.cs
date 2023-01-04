using Riwexoyd.TelegramBotEngine.Polling.Contracts;

namespace Riwexoyd.TelegramBotEngine.Polling.Services
{
    internal sealed class UpdateCounter : IUpdateCounter
    {
        public bool HasUpdates => throw new NotImplementedException();

        public void ReceiveUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
