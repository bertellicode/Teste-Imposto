
using System.Collections.Generic;
using Imposto.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imposto.Domain.Test
{
    /// <summary>
    /// Summary description for NotaFiscalServiceTest
    /// </summary>
    [TestClass]
    public class NotaFiscalItemTest
    {
        private NotaFiscalItem notaFiscalItem;

        public NotaFiscalItemTest()
        {
        }
  
        /// <summary>
        /// Testa o calculo do CFOP.
        /// </summary>
        [TestMethod]
        public void CalcularCfopTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("RJ", "6.000"),
                new KeyValuePair<string, string>("PE", "6.001"),
                new KeyValuePair<string, string>("MG", "6.002"),
                new KeyValuePair<string, string>("PB", "6.003"),
                new KeyValuePair<string, string>("PR", "6.004"),
                new KeyValuePair<string, string>("PI", "6.005"),
                new KeyValuePair<string, string>("RO", "6.006"),
                new KeyValuePair<string, string>("TO", "6.008"),
                new KeyValuePair<string, string>("SE", "6.009"),
                new KeyValuePair<string, string>("PA", "6.010")
            };

            foreach (KeyValuePair<string, string> x in list)
            {
                notaFiscalItem.CalcularCfop(x.Key);

                Assert.AreEqual(x.Value, notaFiscalItem.Cfop, "Confere o valor aferido para o Cfop de acordo com o estado de destino.");
            }

        }

        /// <summary>
        /// Testa o calculo do Tipo do ICMS.
        /// </summary>
        [TestMethod]
        public void CalcularTipoIcmsTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            var estadoOrigem = "RJ";
            var estadoDestino1 = "RJ";
            var estadoDestino2 = "SP";

            notaFiscalItem.CalcularTipoIcms(estadoOrigem, estadoDestino1);

            Assert.AreEqual("60", notaFiscalItem.TipoIcms, "Confere se quando o estado de origem é igual ao de origem o valor calculado é 60.");

            notaFiscalItem.CalcularTipoIcms(estadoOrigem, estadoDestino2);

            Assert.AreNotEqual("60", notaFiscalItem.TipoIcms, "Confere se quando o estado de origem é igual ao de origem o valor calculado não é 60.");
            Assert.AreEqual("10", notaFiscalItem.TipoIcms, "Confere se quando o estado de origem é igual ao de origem o valor calculado é 10.");
        }

        /// <summary>
        /// Testa o calculo da Aliquota de ICMS.
        /// </summary>
        [TestMethod]
        public void CalcularAliquotaIcmsTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            var estadoOrigem = "RJ";
            var estadoDestino1 = "RJ";
            var estadoDestino2 = "SP";

            notaFiscalItem.CalcularAliquotaIcms(estadoOrigem, estadoDestino1);

            Assert.AreEqual((decimal)0.18, notaFiscalItem.AliquotaIcms, "Confere se quando o estado de origem é igual ao de origem o valor calculado é 0.18.");

            notaFiscalItem.CalcularAliquotaIcms(estadoOrigem, estadoDestino2);

            Assert.AreNotEqual((decimal)0.18, notaFiscalItem.AliquotaIcms, "Confere se quando o estado de origem é igual ao de origem o valor calculado não é 0.18.");
            Assert.AreEqual((decimal)0.17, notaFiscalItem.AliquotaIcms, "Confere se quando o estado de origem é igual ao de origem o valor calculado é 0.17.");
        }

        /// <summary>
        /// Testa o calculo da Base do ICMS.
        /// </summary>
        [TestMethod]
        public void CalcularBaseIcmsTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.Cfop = "6.000";
            notaFiscalItem.BaseIcms = 100;

            notaFiscalItem.CalcularBaseIcms();

            Assert.AreEqual(100, notaFiscalItem.BaseIcms, "Confere se o valor da BaseIcms não foi alterado.");

            notaFiscalItem.Cfop = "6.009";

            notaFiscalItem.CalcularBaseIcms();

            Assert.AreEqual((decimal)90, notaFiscalItem.BaseIcms, "Confere se o valor da BaseIcms foi calculado para 90.");
        }

        /// <summary>
        /// Testa o calculo do Valor do ICMS.
        /// </summary>
        [TestMethod]
        public void CalcularValorIcmsTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.BaseIcms = 100;
            notaFiscalItem.AliquotaIcms = (decimal) 0.18;

            notaFiscalItem.CalcularValorIcms();

            Assert.AreEqual(18, notaFiscalItem.ValorIcms, "Confere se o ValorIcms foi calculado para 18.");
        }

        /// <summary>
        /// Testa o calculo realizado caso o pedido item for brinde.
        /// </summary>
        [TestMethod]
        public void CalcularItemPedidoBrindeTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            var brinde = true;
            notaFiscalItem.BaseIcms = 100;

            notaFiscalItem.CalcularItemPedidoBrinde(brinde);

            Assert.AreEqual("60", notaFiscalItem.TipoIcms, "Confere se o TipoIcms foi alterado para 60.");
            Assert.AreEqual((decimal)0.18, notaFiscalItem.AliquotaIcms, "Confere se o TipoIcms foi alterado para 0.18.");
            Assert.AreEqual(18, notaFiscalItem.ValorIcms, "Confere se o ValorIcms foi calculado para 18.");
            Assert.AreEqual(0, notaFiscalItem.AliquotaIpi, "Confere se a AliquotaIpi foi alterada para 0.");
        }

        /// <summary>
        /// Testa o calculo do Valor do IPI.
        /// </summary>
        [TestMethod]
        public void CalcularValorIpiTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.BaseCalculoIpi = 100;
            notaFiscalItem.AliquotaIpi = (decimal) 0.10;

            notaFiscalItem.CalcularValorIpi();

            Assert.AreEqual(10, notaFiscalItem.ValorIpi, "Confere se o ValorIpi foi calculado para 10.");
        }

        /// <summary>
        /// Testa o calculo do percentual de desconto.
        /// </summary>
        [TestMethod]
        public void CalcularPercentualDescontoTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            var estadoDestino = "RO";

            notaFiscalItem.CalcularPercentualDesconto(estadoDestino);

            Assert.AreNotEqual(0.10, notaFiscalItem.Desconto, "Confere se o Desconto não foi alterado para 0.10.");

            estadoDestino = "MG";

            notaFiscalItem.CalcularPercentualDesconto(estadoDestino);

            Assert.AreNotEqual(0, notaFiscalItem.Desconto, "Confere se o Desconto foi alterado para um valor diferente de 0.");
        }
    }
}
