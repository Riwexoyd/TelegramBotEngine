namespace Riwexoyd.TelegramBotEngine.Polling.Contracts
{
    /// <summary>
    /// Сервис получения обновлений
    /// </summary>
    internal interface IUpdateReceiverService
    {
        /// <summary>
        /// Получить асинхронно обновления
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Асинхронная операция</returns>
        Task ReceiveAsync(CancellationToken cancellationToken);
    }
}