using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using Imposto.Infra.Data.Contexto;
using Imposto.Infra.Data.Interfaces;

namespace Imposto.Infra.Data.UoW
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly TesteImpostoContext _context;
        private DbContextTransaction _dbContextTransaction;
        private bool _disposed;

        public UnitOfWork(TesteImpostoContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        public void BeginTransactionQuery()
        {
            _dbContextTransaction = _context.Database.BeginTransaction();
        }

        public void RollbackTransactionQuery()
        {
            _dbContextTransaction.Rollback();
        }

        public void CommitQuery()
        {
            try
            {
                _context.SaveChanges();
                _dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                _dbContextTransaction.Rollback();
                OnError(ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void OnError(Exception ex)
        {
            if (typeof(DbEntityValidationException) == ex.GetType())
            {
                var newEx = ex as DbEntityValidationException;
                throw new Exception(newEx.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage);
            }
            else if (typeof(DbUpdateException) == ex.GetType())
            {
                var newEx = ex as DbUpdateException;
                if (newEx.InnerException != null)
                {
                    if (newEx.InnerException.InnerException != null)
                    {
                        throw newEx.InnerException.InnerException;
                    }
                    throw newEx.InnerException;
                }
                throw newEx;
            }
            else if (typeof(NullReferenceException) == ex.GetType())
            {
                var newEx = ex as NullReferenceException;
                throw newEx.InnerException.InnerException;
            }
            throw ex;
        }

    }
}
