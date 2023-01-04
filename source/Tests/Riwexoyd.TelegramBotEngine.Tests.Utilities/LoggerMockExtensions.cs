using Microsoft.Extensions.Logging;

using Moq;

namespace Riwexoyd.TelegramBotEngine.Tests.Utilities
{
    public static class LoggerMockExtensions
    {
        public static Moq.Language.Flow.ISetup<ILogger<T>> SetupLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel? logLevel = null)
        {
            var setup = loggerMock.Setup(logger => 
                logger.Log(logLevel ?? It.IsAny<LogLevel>(), 
                    It.IsAny<EventId>(), 
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception?>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()));

            return setup;
        }

        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel? logLevel = null, Times? times = null)
        {
            loggerMock.Verify(logger =>
                logger.Log(logLevel ?? It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception?>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()), times ?? Times.Once());
        }
    }
}