using System.Collections.Generic;
using AutoMapper;
using Imposto.Application.AutoMapper;
using Imposto.Application.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Imposto.Domain.NotaFiscalAggregate.Entities;

namespace Imposto.Application.Test
{
    [TestClass]
    public class NotaFiscalAppServiceTest
    {

        PedidoViewModel pedido;

        public NotaFiscalAppServiceTest()
        {
            PopularPedido();

            AutoMapperConfig.RegisterMappings();
        }

        /// <summary>
        /// Popular o PedidoViewModel que será traduzido para o modelo de domínio Nota Fiscal. 
        /// </summary>
        private void PopularPedido()
        {
            pedido = new PedidoViewModel
            {
                NomeCliente = "Cliente Teste Mapeamento",
                EstadoOrigem = "MG",
                EstadoDestino = "RJ",
                ItensDoPedido = new List<PedidoItemViewModel>() { new PedidoItemViewModel()
                {
                    ValorItemPedido = 100,
                    CodigoProduto = "123-5548-555-00",
                    NomeProduto = "Produto Teste Mapeamento",
                    Brinde = false
                } }
            };
        }

        /// <summary>
        /// Testar o mapeamento 
        /// </summary>
        [TestMethod]
        public void TestarMapeamento()
        {
            var itemPedido = pedido.ItensDoPedido.FirstOrDefault();
            var notaFiscal = Mapper.Map<PedidoViewModel, NotaFiscal>(pedido);
            var notaFiscalItem = notaFiscal.ItensDaNotaFiscal.FirstOrDefault();

            Assert.AreEqual(99999, notaFiscal.NumeroNotaFiscal);

            Assert.AreEqual(pedido.NomeCliente, notaFiscal.NomeCliente);

            Assert.AreEqual(pedido.EstadoOrigem, notaFiscal.EstadoOrigem);

            Assert.AreEqual(pedido.EstadoDestino, notaFiscal.EstadoDestino);

            Assert.AreEqual(itemPedido.NomeProduto, notaFiscalItem.NomeProduto);

            Assert.AreEqual(itemPedido.CodigoProduto, notaFiscalItem.CodigoProduto);

            Assert.AreEqual((decimal)itemPedido.ValorItemPedido, notaFiscalItem.BaseIcms);

            Assert.AreEqual((decimal)itemPedido.ValorItemPedido, notaFiscalItem.BaseCalculoIpi);

            Assert.AreEqual((decimal)0.10, notaFiscalItem.AliquotaIpi);

            Assert.AreEqual("6.000", notaFiscalItem.Cfop);

            Assert.AreEqual("10", notaFiscalItem.TipoIcms);

            Assert.AreEqual((decimal)0.17, notaFiscalItem.AliquotaIcms);

            Assert.AreEqual((decimal)itemPedido.ValorItemPedido, notaFiscalItem.BaseIcms);

            Assert.AreEqual(17, notaFiscalItem.ValorIcms);

            Assert.AreEqual(10, notaFiscalItem.ValorIpi);

            Assert.AreEqual((decimal)0.10, notaFiscalItem.Desconto);

        }
    }
}
