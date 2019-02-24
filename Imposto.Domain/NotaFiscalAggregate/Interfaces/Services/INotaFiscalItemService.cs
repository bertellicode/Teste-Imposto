using System;
using Imposto.Domain.NotaFiscalAggregate.Entities;

namespace Imposto.Domain.NotaFiscalAggregate.Interfaces.Services
{
    public interface INotaFiscalItemService : IDisposable
    {
        /// <summary>
        /// Chamar a camada de persistência do banco de dados.
        /// </summary>
        /// <param name="notaFiscalItem"></param>
        /// <returns></returns>
        bool Salvar(NotaFiscalItem notaFiscalItem);

    }
}
