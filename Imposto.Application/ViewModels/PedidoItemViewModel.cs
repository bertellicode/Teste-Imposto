namespace Imposto.Application.ViewModels
{
    /// <summary>
    /// Responsável por representar os dados do item de pedido na tela.
    /// </summary>
    public class PedidoItemViewModel
    {
        public string NomeProduto { get; set; }
        public string CodigoProduto { get; set; }        
        public decimal? ValorItemPedido { get; set; }
        public bool Brinde { get; set; }        
    }
}
