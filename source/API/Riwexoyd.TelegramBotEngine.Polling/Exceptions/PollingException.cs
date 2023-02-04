using System.Runtime.Serialization;

namespace Riwexoyd.TelegramBotEngine.Polling.Exceptions
{
    [Serializable]
    public class PollingException : Exception
    {
        public PollingException()
        {
        }

        public PollingException(string? message) : base(message)
        {
        }

        public PollingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected PollingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
