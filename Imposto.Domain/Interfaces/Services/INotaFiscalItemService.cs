using System;
using Imposto.Domain.Entities;

namespace Imposto.Domain.Interfaces.Services
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
