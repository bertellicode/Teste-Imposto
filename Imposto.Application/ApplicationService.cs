
using Imposto.Infra.Data.Interfaces;

namespace Imposto.Application
{
    /// <summary>
    /// Encapsula as funcionalidades do UnitOfWork.
    /// </summary>
    public class ApplicationService
    {
        private readonly IUnitOfWork _uow;

        public ApplicationService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        public void Commit()
        {
            _uow.Commit();
        }

        public void BeginTransactionQuery()
        {
            _uow.BeginTransactionQuery();
        }

        public void CommitQuery()
        {
            _uow.CommitQuery();
        }

    }
}
