namespace Riwexoyd.TelegramBotEngine.Core.Models
{
    public enum TelegramUpdateType
    {
        Unknown,
        Message,
        InlineQuery,
        ChosenInlineResult,
        CallbackQuery,
        EditedMessage,
        ChannelPost,
        EditedChannelPost,
        ShippingQuery,
        PreCheckoutQuery,
        Poll,
        PollAnswer,
        MyChatMember,
        ChatMember,
        ChatJoinRequest,
    }
}
