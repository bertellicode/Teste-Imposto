using FluentValidation.Results;
using Imposto.Domain.Core.Interfaces;
using Imposto.Domain.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Domain.Core.Services
{
    public class Service : IService
    {

        private readonly INotificationHandler _notificationHandler;

        public Service(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        public void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notificationHandler.Handle(new Notification(error.PropertyName, error.ErrorMessage));
            }
        }

        public void NotificarValidacao(string propertyName = null, string errorMessage = null)
        {
            _notificationHandler.Handle(new Notification(propertyName, errorMessage));
        }

        public bool HasNotifications()
        {
            return _notificationHandler.HasNotifications();
        }
    }
}
