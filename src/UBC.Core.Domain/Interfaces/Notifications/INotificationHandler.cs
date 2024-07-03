namespace UBC.Core.Domain.Interfaces.Notifications
{
    public interface INotificationHandler<T> where T : class
    {
        Task Handler(T notification);
    }
}
