using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Imposto.Domain.Interfaces.Repositories;
using Imposto.Infra.Data.Contexto;

namespace Imposto.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected TesteImpostoContext _Db;

        public RepositoryBase(TesteImpostoContext context)
        {
            _Db = context;
        }

   
        public TEntity GetById(int id)
        {
            return _Db.Set<TEntity>().Find(id);
        }


        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _Db.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList<TEntity>();
        }

      
        public TEntity Add(TEntity obj)
        {
            var objInserted = _Db.Set<TEntity>().Add(obj);
            return objInserted;
        }

    
        public TEntity Update(TEntity obj)
        {
            _Db.Entry(obj).State = EntityState.Modified;
            return obj;
        }

  
        public TEntity Update(TEntity objDb, TEntity obj)
        {
            _Db.Entry(objDb).CurrentValues.SetValues(obj);
            return obj;
        }

      
        public void Remove(int id)
        {
            var obj = GetById(id);
            _Db.Set<TEntity>().Remove(obj);
            _Db.SaveChanges();
        }

        public void Dispose()
        {
            _Db.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
