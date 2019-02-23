using Imposto.Domain.Entities;

namespace Imposto.Domain.Interfaces.Repositories
{
    public interface INotaFiscalItemRepository : IRepositoryBase<NotaFiscalItem>
    {

        /// <summary>
        /// Responsável por persistir um item da nota fiscal.
        /// </summary>
        /// <param name="notaFiscalItem"></param>
        /// <returns>Status se a operação deu certo ou errado.</returns>
        bool Salvar(NotaFiscalItem notaFiscalItem);

    }
}
