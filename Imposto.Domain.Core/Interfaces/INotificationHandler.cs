using Imposto.Domain.Core.Notifications;
using System.Collections.Generic;

namespace Imposto.Domain.Core.Interfaces
{
    public interface INotificationHandler
    {
        void InitializeNotifications();

        List<Notification> GetNotifications();

        void Handle(Notification message);

        bool HasNotifications();
    }
}
