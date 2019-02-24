using System;
using AutoMapper;
using Imposto.Application.ViewModels;
using Imposto.Domain.NotaFiscalAggregate.Entities;

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
                    BaseIcms = itemPedido.ValorItemPedido,
                    BaseCalculoIpi = itemPedido.ValorItemPedido,
                    AliquotaIpi = (decimal)0.10
                };

                notafiscal.AdicionarItemDaNotaFiscal(notaFiscalItem, itemPedido.Brinde);
            }

            return notafiscal;

        }
    }
}
