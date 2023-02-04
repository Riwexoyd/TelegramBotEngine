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

namespace Riwexoyd.TelegramBotEngine.Polling.Services
{
    internal sealed class UpdateReceiverService : IUpdateReceiverService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IUpdateHandler _updateHandler;
        private readonly ILogger<UpdateReceiverService> _logger;
        private readonly IMapper _mapper;
        private readonly UpdateType[] _allowedUpdate;

        public UpdateReceiverService(ITelegramBotClient telegramBotClient,
            IUpdateHandler updateHandler,
            ILogger<UpdateReceiverService> logger,
            IOptions<TelegramBotConfiguration> botConfiguration,
            IMapper mapper)
        {
            _telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
            _updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            TelegramBotConfiguration telegramBotConfiguration = botConfiguration?.Value ?? throw new ArgumentNullException(nameof(botConfiguration));
            if (telegramBotConfiguration.AllowedUpdates != null)
            {
                _allowedUpdate = _mapper.Map<TelegramUpdateType[], UpdateType[]>(telegramBotConfiguration.AllowedUpdates);
            }
            else
            {
                _allowedUpdate = Array.Empty<UpdateType>();
            }
        }

        /// <inheritdoc/>
        public async Task ReceiveAsync(CancellationToken cancellationToken)
        {
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = _allowedUpdate,
                ThrowPendingUpdates = true
            };

            User me = await _telegramBotClient.GetMeAsync(cancellationToken)
                .ConfigureAwait(false);
            _logger.LogInformation("Start receiving updates for {BotName}", me.Username);

            await _telegramBotClient.ReceiveAsync(
                updateHandler: _updateHandler,
                receiverOptions: receiverOptions,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}