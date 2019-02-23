using Imposto.Domain.Entities;

namespace Imposto.Domain.Interfaces.Repositories
{
    public interface INotaFiscalRepository : IRepositoryBase<NotaFiscal>
    {

        /// <summary>
        /// Persiste o XML em uma pasta do sistema.
        /// </summary>
        /// <param name="notaFiscal"></param>
        /// <returns>Status se a operação foi realizada com sucesso.</returns>
        bool SalvarXml(NotaFiscal notaFiscal);

        /// <summary>
        /// Persiste o resgistro no banco de dados.
        /// </summary>
        /// <param name="notaFiscal"></param>
        /// <returns>Retorna o id caso a operação de certo e 0 se der errado.</returns>
        int Salvar(NotaFiscal notaFiscal);

    }
}
