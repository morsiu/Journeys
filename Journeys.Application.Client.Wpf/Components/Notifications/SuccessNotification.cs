namespace Journeys.Application.Client.Wpf.Components.Notifications
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
