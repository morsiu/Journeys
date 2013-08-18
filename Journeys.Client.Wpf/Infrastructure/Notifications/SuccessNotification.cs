namespace Journeys.Client.Wpf.Infrastructure.Notifications
{
    internal class SuccessNotification
    {
        public SuccessNotification(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
