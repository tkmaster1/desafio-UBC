using UBC.Core.Domain.Notifications;

namespace UBC.Core.Domain.Interfaces.Notifications
{
    public interface INotificador
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Handle(Notification notificacao);
    }
}
