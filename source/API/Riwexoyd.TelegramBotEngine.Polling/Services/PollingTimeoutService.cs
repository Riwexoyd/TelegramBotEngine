using Microsoft.Extensions.Options;

using Riwexoyd.TelegramBotEngine.Polling.Configurations;
using Riwexoyd.TelegramBotEngine.Polling.Contracts;

namespace Riwexoyd.TelegramBotEngine.Polling.Services
{
    internal sealed class PollingTimeoutService : IPollingTimeoutService
    {
        private readonly IUpdateCounterService _updateCounterService;
        private readonly int[]? _updateTimeoutMillisecondsCollection;
        private int _currentTime = 0;

        public PollingTimeoutService(IOptions<TelegramBotPollingConfiguration> pollingConfigurationOptions, IUpdateCounterService updateCounterService)
        {
            _updateCounterService = updateCounterService ?? throw new ArgumentNullException(nameof(updateCounterService));

            if (pollingConfigurationOptions == null)
                throw new ArgumentNullException(nameof(pollingConfigurationOptions));
            _updateTimeoutMillisecondsCollection = pollingConfigurationOptions.Value.UpdateTimeoutMillisecondsCollection;
        }

        public Task WaitAsync(CancellationToken cancellationToken)
        {
            int timeOut = GetWaitTime();

            if (timeOut == 0)
                return Task.CompletedTask;
            else
                return Task.Delay(timeOut, cancellationToken);
        }

        private int GetWaitTime()
        {
            if (_updateTimeoutMillisecondsCollection == null)
                return 0;

            if (!_updateCounterService.HasUpdates)
            {
                _currentTime = Math.Min(_updateTimeoutMillisecondsCollection.Length - 1, _currentTime + 1);
            }
            else
            {
                _currentTime = 0;
            }                
                
            return _updateTimeoutMillisecondsCollection[_currentTime];
        }
    }
}
