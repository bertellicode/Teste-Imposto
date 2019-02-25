
using System.Collections.Generic;
using Imposto.Domain.Core.Interfaces;
using Imposto.Domain.Core.Notifications;
using Imposto.Infra.Data.Interfaces;

namespace Imposto.Application
{
    /// <summary>
    /// Encapsula as funcionalidades do UnitOfWork e do Notification
    /// </summary>
    public class ApplicationService
    {
        private readonly IUnitOfWork _uow;
        private readonly INotificationHandler _notificationHandler;

        public ApplicationService(IUnitOfWork uow,
                                    INotificationHandler notificationHandler)
        {
            _uow = uow;
            _notificationHandler = notificationHandler;
        }

        public void BeginTransactionQuery()
        {
            _uow.BeginTransactionQuery();
        }

        public bool CommitQuery()
        {
            if (_notificationHandler.HasNotifications())
            {
                _uow.RollbackTransactionQuery();
                return false;
            }

            _uow.CommitQuery();

            return false;
        }

        public List<Notification> GetNotifications()
        {
            return _notificationHandler.GetNotifications();
        }

        public void InitializeNotifications()
        {
            _notificationHandler.InitializeNotifications();
        }

    }
}
