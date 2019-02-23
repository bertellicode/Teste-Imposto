using System.Collections.Generic;

namespace Imposto.Application.ViewModels
{
    /// <summary>
    /// Responsável por representar os dados do pedido na tela.
    /// </summary>
    public class PedidoViewModel
    {
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public string NomeCliente { get; set; }

        public List<PedidoItemViewModel> ItensDoPedido { get; set; }

        public PedidoViewModel()
        {
            ItensDoPedido = new List<PedidoItemViewModel>();
        }
    }
}
