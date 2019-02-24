using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Imposto.Domain.NotaFiscalAggregate.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Recupera um registro pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Recupera uma lista de registros.
        /// </summary>
        /// <param name="filter">Expression lambda para filtrar os dados.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null );

        /// <summary>
        /// Salvar um registro no banco.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        TEntity Add(TEntity obj);

        /// <summary>
        /// Atualizar um regsitro no banco.
        /// </summary>
        /// <param name="obj">Objeto que será alterado.</param>
        /// <returns></returns>
        TEntity Update(TEntity obj);

        /// <summary>
        /// Atualizar um regsitro no banco.
        /// </summary>
        /// <param name="objDb">Registro existente. Objeto no contexto de transação.</param>
        /// <param name="obj">Registro novo. Objeto fora do contexto de transação.</param>
        /// <returns></returns>
        TEntity Update(TEntity objDb, TEntity obj);

        /// <summary>
        /// Exclui um registro com base no ID.
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);
        void Dispose();
    }
}
