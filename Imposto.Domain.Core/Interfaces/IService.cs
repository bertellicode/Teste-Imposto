using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Domain.Core.Interfaces
{
    public interface IService
    {
        void NotificarValidacoesErro(ValidationResult validationResult);

        void NotificarValidacao(string propertyName = null, string errorMessage = null);

        bool HasNotifications();
    }
}
