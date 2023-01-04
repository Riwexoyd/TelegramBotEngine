using Riwexoyd.TelegramBotEngine.Core.Models;

namespace Riwexoyd.TelegramBotEngine.Core.Configurations
{
    public sealed class TelegramBotConfiguration
    {
        public string Token { get; set; } = string.Empty;

        public TelegramUpdateType[]? AllowedUpdates { get; set; } = Array.Empty<TelegramUpdateType>();
    }
}
