using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Riwexoyd.TelegramBotEngine.Polling.Contracts;

namespace Riwexoyd.TelegramBotEngine.Polling.Hosting.Services
{
    internal sealed class PollingBackgroundService : BackgroundService
    {
        private readonly ILogger<PollingBackgroundService> _logger;
        private readonly IPollingService _pollingService;

        public PollingBackgroundService(ILogger<PollingBackgroundService> logger, IPollingService pollingService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pollingService = pollingService ?? throw new ArgumentNullException(nameof(pollingService));
        }

        /// <inheritdoc/>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Polling service started");

            return _pollingService.PollAsync(stoppingToken);
        }
    }
}