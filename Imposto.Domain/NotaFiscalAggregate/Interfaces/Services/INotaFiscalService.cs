using System;
using Imposto.Domain.NotaFiscalAggregate.DTOs;
using Imposto.Domain.NotaFiscalAggregate.Entities;

namespace Imposto.Domain.NotaFiscalAggregate.Interfaces.Services
{
    public interface INotaFiscalService : IDisposable
    {
        /// <summary>
        /// Responsável por comunicar com a camada de persistência do Banco de Dados.
        /// </summary>
        /// <param name="notaFiscal">Nota Fiscal</param>
        /// <returns>Id da Nota Fiscal</returns>
        int? Salvar(NotaFiscal notaFiscal);

        /// <summary>
        /// Responsável por comunicar com a camada de persistência do XML
        /// </summary>
        /// <param name="notaFiscal">Nota Fiscal DTO</param>
        /// <returns>Status da operação sucesso/falha</returns>
        bool GerarXml(NotaFiscalXmlDto notaFiscal);
    }
}
