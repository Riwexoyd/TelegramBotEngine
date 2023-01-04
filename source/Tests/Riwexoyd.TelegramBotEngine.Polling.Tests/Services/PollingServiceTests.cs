using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

using Riwexoyd.TelegramBotEngine.Polling.Configurations;
using Riwexoyd.TelegramBotEngine.Polling.Contracts;
using Riwexoyd.TelegramBotEngine.Polling.Services;
using Riwexoyd.TelegramBotEngine.Tests.Utilities;

using Xunit;

namespace Riwexoyd.TelegramBotEngine.Polling.Tests.Services
{
    public sealed class PollingServiceTests
    {
        private readonly PollingService _pollingService;
        private readonly Mock<IServiceProvider> _serviceProviderMock = new();
        private readonly Mock<ILogger<PollingService>> _logger = new();
        private readonly Mock<IOptions<TelegramBotPollingConfiguration>> _pollingConfigurationOptions = new();
        private readonly Mock<IPollingTimeoutService> _pollingTimeoutService = new();
        private readonly Mock<IUpdateReceiverService> _updateReceiverService = new();
        private readonly Mock<IServiceScopeFactory> _serviceScopeFactory = new();

        public PollingServiceTests()
        {
            _pollingService = new(_serviceProviderMock.Object,
                _logger.Object,
                _pollingConfigurationOptions.Object,
                _pollingTimeoutService.Object);

            _pollingConfigurationOptions.Setup(options => options.Value)
                .Returns(new TelegramBotPollingConfiguration());

            _serviceProviderMock.Setup(serviceProvider => serviceProvider.GetService(typeof(IServiceScopeFactory)))
                .Returns(_serviceScopeFactory.Object);

            Mock<IServiceScope> serviceScopeMock = new();

            _serviceScopeFactory.Setup(factory => factory.CreateScope())
                .Returns(serviceScopeMock.Object);

            Mock<IServiceProvider> scopeServiceProvider = new();

            serviceScopeMock.Setup(scope => scope.ServiceProvider)
                .Returns(scopeServiceProvider.Object);

            scopeServiceProvider.Setup(serviceProvider => serviceProvider.GetService(typeof(IUpdateReceiverService)))
                .Returns(_updateReceiverService.Object);
        }

        [Fact]
        public async Task PollAsync_MustHandleException()
        {
            // Arrange
            CancellationTokenSource cancellationTokenSource = new();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            _updateReceiverService.SetupSequence(service => service.ReceiveAsync(cancellationToken))
                .ThrowsAsync(new Exception())
                .Returns(Task.CompletedTask);

            _pollingTimeoutService.Setup(service => service.WaitAsync(cancellationToken))
                .Callback(cancellationTokenSource.Cancel);

            // Act
            await _pollingService.PollAsync(cancellationTokenSource.Token);

            // Assert
            _logger.VerifyLog(LogLevel.Error, Times.Once());
        }
    }
}
