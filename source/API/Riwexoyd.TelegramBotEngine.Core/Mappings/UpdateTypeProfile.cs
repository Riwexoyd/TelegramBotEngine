using AutoMapper;

using Riwexoyd.TelegramBotEngine.Core.Models;

using Telegram.Bot.Types.Enums;

namespace Riwexoyd.TelegramBotEngine.Core.Mappings
{
    internal sealed class UpdateTypeProfile : Profile
    {
        public UpdateTypeProfile() 
        {
            CreateMap<TelegramUpdateType, UpdateType>()
                .ConvertUsing(ConvertTelegramUpdateType);
        } 

        private static UpdateType ConvertTelegramUpdateType(TelegramUpdateType telegramUpdateType, UpdateType updateType)
        {
            return telegramUpdateType switch
            {
                TelegramUpdateType.Unknown => UpdateType.Unknown,
                TelegramUpdateType.Message => UpdateType.Message,
                TelegramUpdateType.InlineQuery => UpdateType.InlineQuery,
                TelegramUpdateType.ChosenInlineResult => UpdateType.ChosenInlineResult,
                TelegramUpdateType.CallbackQuery => UpdateType.CallbackQuery,
                TelegramUpdateType.EditedMessage => UpdateType.EditedMessage,
                TelegramUpdateType.ChannelPost => UpdateType.ChannelPost,
                TelegramUpdateType.EditedChannelPost => UpdateType.EditedChannelPost,
                TelegramUpdateType.ShippingQuery => UpdateType.ShippingQuery,
                TelegramUpdateType.PreCheckoutQuery => UpdateType.PreCheckoutQuery,
                TelegramUpdateType.Poll => UpdateType.Poll,
                TelegramUpdateType.PollAnswer => UpdateType.PollAnswer,
                TelegramUpdateType.MyChatMember => UpdateType.MyChatMember,
                TelegramUpdateType.ChatMember => UpdateType.ChatMember,
                TelegramUpdateType.ChatJoinRequest => UpdateType.ChatJoinRequest,
                _ => throw new ArgumentOutOfRangeException(nameof(telegramUpdateType), "Unknown update type"),
            };
        }
    }
}
