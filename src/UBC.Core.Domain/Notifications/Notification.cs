namespace UBC.Core.Domain.Notifications
{
    public class Notification
    {
        public string Message { get; }

        public Notification(string _message)
        {
            Message = _message;
        }
    }
}