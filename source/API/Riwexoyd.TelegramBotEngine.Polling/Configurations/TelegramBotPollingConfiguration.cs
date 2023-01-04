namespace Riwexoyd.TelegramBotEngine.Polling.Configurations
{
    public sealed class TelegramBotPollingConfiguration
    {
        public int PollingErrorWaitTimeMilliseconds { get; set; } = 5000;

        public int[]? UpdateTimeoutMillisecondsCollection { get; set; } = null;
    }
}
