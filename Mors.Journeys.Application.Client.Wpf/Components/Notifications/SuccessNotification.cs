namespace Mors.Journeys.Application.Client.Wpf.Components.Notifications
{
    internal sealed class SuccessNotification
    {
        public SuccessNotification(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
