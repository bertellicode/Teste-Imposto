using Imposto.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imposto.Domain.Core.Notifications
{
    public class NotificationHandler : INotificationHandler
    {
        private List<Notification> _notifications;

        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }

        public virtual void InitializeNotifications()
        {
            _notifications = new List<Notification>();
        }

        public virtual List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public virtual void Handle(Notification message)
        {
            _notifications.Add(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Erro: {message.Key} - {message.Value}");
        }

        public virtual bool HasNotifications()
        {
            return _notifications.Any();
        }
    }
}
