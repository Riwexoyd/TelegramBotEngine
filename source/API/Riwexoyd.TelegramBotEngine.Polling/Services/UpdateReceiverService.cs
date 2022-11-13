using AutoMapper;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Riwexoyd.TelegramBotEngine.Core.Configurations;
using Riwexoyd.TelegramBotEngine.Core.Models;
using Riwexoyd.TelegramBotEngine.Polling.Contracts;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Riwexoyd.TelegramBotEngine.Core.Services
{
    public sealed class UpdateReceiverService : IUpdateReceiverService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IUpdateHandler _updateHandler;
        private readonly ILogger<UpdateReceiverService> _logger;
        private readonly BotConfiguration _botConfiguration;
        private readonly IMapper _mapper;

        public UpdateReceiverService(ITelegramBotClient telegramBotClient,
            IUpdateHandler updateHandler,
            ILogger<UpdateReceiverService> logger,
            IOptions<BotConfiguration> botConfiguration,
            IMapper mapper)
        {
            _telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _botConfiguration = botConfiguration?.Value ?? throw new ArgumentNullException(nameof(botConfiguration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task ReceiveAsync(CancellationToken cancellationToken)
        {
            UpdateType[] allowedUpdate = _mapper.Map<TelegramUpdateType[], UpdateType[]>(_botConfiguration.AllowedUpdates);

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = allowedUpdate,
                ThrowPendingUpdates = true
            };

            User me = await _telegramBotClient.GetMeAsync(cancellationToken);
            _logger.LogInformation("Start receiving updates for {BotName}", me.Username);

            await _telegramBotClient.ReceiveAsync(
                updateHandler: _updateHandler,
                receiverOptions: receiverOptions,
                cancellationToken: cancellationToken);
        }
    }
}