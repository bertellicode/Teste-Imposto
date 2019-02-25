using System;
using System.Collections.Generic;
using Imposto.Application.ViewModels;
using Imposto.Domain.Core.Notifications;

namespace Imposto.Application.Interfaces
{
    public interface INotaFiscalAppService : IDisposable
    {

        /// <summary>
        /// Responsável por gerar a nota fiscal a partir do pedido.
        /// </summary>
        /// <param name="pedido">PedidoViewModel</param>
        /// <returns>Retorna uma lista de erros para serem exibidos.</returns>
        List<Notification> GerarNotaFiscal(PedidoViewModel pedido);

    }
}
