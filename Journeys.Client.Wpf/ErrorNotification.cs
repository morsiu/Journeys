namespace Journeys.Client.Wpf
{
    internal class ErrorNotification
    {
        public ErrorNotification(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
