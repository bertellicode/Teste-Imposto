using System;
using AutoMapper;
using Imposto.Application.ViewModels;
using Imposto.Domain.Entities;

namespace Imposto.Application.AutoMapper.CustomTypeConverter
{
    /// <summary>
    /// Responsável por converter o PedidoViewModel em NotaFiscal.
    /// Traduz o modelo da camada de apresentação em modelo de domínio.
    /// </summary>
    class PedidoVwToNotaFiscalConverter : ITypeConverter<PedidoViewModel, NotaFiscal>
    {
        public NotaFiscal Convert(PedidoViewModel pedido, NotaFiscal notafiscal, ResolutionContext context)
        {

            notafiscal = new NotaFiscal
            {
                NumeroNotaFiscal = 99999,
                Serie = new Random().Next(Int32.MaxValue),
                NomeCliente = pedido.NomeCliente,
                EstadoDestino = pedido.EstadoDestino,
                EstadoOrigem = pedido.EstadoOrigem,
            };

            foreach (PedidoItemViewModel itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem
                {
                    NomeProduto = itemPedido.NomeProduto,
                    CodigoProduto = itemPedido.CodigoProduto,
                    BaseIcms = (decimal) itemPedido.ValorItemPedido,
                    BaseCalculoIpi = (decimal) itemPedido.ValorItemPedido,
                    AliquotaIpi = (decimal) 0.10
                };

                notaFiscalItem.CalcularCfop(pedido.EstadoDestino);

                notaFiscalItem.CalcularTipoIcms(pedido.EstadoOrigem, pedido.EstadoDestino);

                notaFiscalItem.CalcularAliquotaIcms(pedido.EstadoOrigem, pedido.EstadoDestino);

                notaFiscalItem.CalcularBaseIcms();

                notaFiscalItem.CalcularValorIcms();

                notaFiscalItem.CalcularItemPedidoBrinde(itemPedido.Brinde);

                notaFiscalItem.CalcularValorIpi();

                notaFiscalItem.CalcularPercentualDesconto(pedido.EstadoDestino);

                notafiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            return notafiscal;

        }
    }
}
