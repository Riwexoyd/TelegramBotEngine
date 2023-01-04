using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Riwexoyd.TelegramBotEngine.Polling.Configurations;
using Riwexoyd.TelegramBotEngine.Polling.Contracts;

namespace Riwexoyd.TelegramBotEngine.Polling.Services
{
    internal sealed class PollingService : IPollingService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PollingService> _logger;
        private readonly IOptions<TelegramBotPollingConfiguration> _pollingConfigurationOptions;
        private readonly IPollingTimeoutService _pollingTimeoutService;

        public PollingService(IServiceProvider serviceProvider,
            ILogger<PollingService> logger,
            IOptions<TelegramBotPollingConfiguration> pollingConfigurationOptions,
            IPollingTimeoutService pollingTimeoutService)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pollingConfigurationOptions = pollingConfigurationOptions ?? throw new ArgumentNullException(nameof(pollingConfigurationOptions));
            _pollingTimeoutService = pollingTimeoutService ?? throw new ArgumentNullException(nameof(pollingTimeoutService));
        }

        public async Task PollAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var serviceProvider = scope.ServiceProvider;
                    IUpdateReceiverService receiver = serviceProvider.GetRequiredService<IUpdateReceiverService>();

                    await receiver.ReceiveAsync(cancellationToken)
                        .ConfigureAwait(false);

                    await _pollingTimeoutService.WaitAsync(cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "{PollingService} update receiving error", nameof(PollingService));

                    var timeOut = _pollingConfigurationOptions.Value.PollingErrorWaitTimeMilliseconds;
                    await Task.Delay(timeOut, cancellationToken);
                }
            }
        }
    }
}
