using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Imposto.Domain.Core.Entities
{
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T>
    {
        protected Entity()
        {
            ValidationResult = new ValidationResult();
        }

        public int Id { get; set; }

        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool Validar();

        public bool EhValido => ValidationResult.IsValid;

        public override string ToString() => GetType().Name + "[Id = " + Id + "]";
    }
}
