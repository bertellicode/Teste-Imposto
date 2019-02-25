
namespace Imposto.Infra.Data.Interfaces
{
    /// <summary>
    /// Responsável por manter um único contexto em aberto com o banco de dados.
    /// </summary>
    public interface IUnitOfWork
    {
        void BeginTransaction();

        /// <summary>
        /// Salva as alterações feitas nesse contexto do banco.
        /// </summary>
        void Commit();

        /// <summary>
        /// Métdodo responsável por iniciar uma transação com o banco de dados.
        /// </summary>
        void BeginTransactionQuery();

        /// <summary>
        /// Métdodo responsável por encerrar uma transação com o banco de dados.
        /// </summary>
        void CommitQuery();

        /// <summary>
        /// Método responsável por desfazer as alterações no banco e fechar a transação corrente
        /// </summary>
        void RollbackTransactionQuery();
    }
}
