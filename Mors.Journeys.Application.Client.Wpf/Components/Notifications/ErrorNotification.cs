namespace Mors.Journeys.Application.Client.Wpf.Components.Notifications
{
    internal sealed class ErrorNotification
    {
        public ErrorNotification(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
