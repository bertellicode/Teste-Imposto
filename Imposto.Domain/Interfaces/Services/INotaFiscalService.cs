using System;
using Imposto.Domain.Entities;

namespace Imposto.Domain.Interfaces.Services
{
    public interface INotaFiscalService : IDisposable
    {
        /// <summary>
        /// Responsável por comunicar com a camada de persistência do XML e do Banco de Dados.
        /// </summary>
        /// <param name="notaFiscal"></param>
        /// <returns></returns>
        int Salvar(NotaFiscal notaFiscal);

    }
}
