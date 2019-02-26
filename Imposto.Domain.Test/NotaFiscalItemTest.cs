
using System.Collections.Generic;
using System.Globalization;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Domain.NotaFiscalAggregate.Enums;
using Imposto.Infra.CrossCutting.Util;
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

        #region Calcular CFOP

        /// <summary>
        /// Testa o cálculo do CFOP com valores válidos.
        /// </summary>
        [TestMethod]
        public void CalcularCfopTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            var list = EnumUtil.GetEnumSelectListKeyAndValue<EstadoCfopEnum>("0,0", CultureInfo.CreateSpecificCulture("el-GR"));

            foreach (KeyValuePair<string, string> x in list)
            {
                notaFiscalItem.CalcularCfopPorEstado(x.Key);

                Assert.AreEqual(x.Value, notaFiscalItem.Cfop, "Erro ao calcular CFOP com entrada válida.");
            }

        }

        /// <summary>
        /// Testa o cálculo do CFOP com valores inválidos.
        /// </summary>
        [TestMethod]
        public void CalcularCfopEstadoInvalidoTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.CalcularCfopPorEstado("SP");

            Assert.AreEqual(null, notaFiscalItem.Cfop, "Erro ao calcular CFOP com entrada inválida.");
        }

        /// <summary>
        /// Teste o cálculo do CFOP com entrada nula.
        /// </summary>
        [TestMethod]
        public void CalcularCfopEstadoNuloTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.CalcularCfopPorEstado(null);

            Assert.AreEqual(null, notaFiscalItem.Cfop, "Erro ao calcular CFOP com entrada nula.");
        }

        #endregion

        #region Calcular Tipo ICMS

        /// <summary>
        /// Testa o calculo do Tipo do ICMS quando estado de Origem e Destino são iguais.
        /// </summary>
        [TestMethod]
        public void CalcularTipoIcmsEstadoOrigemDestinoIguaisTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            var estadoOrigem = "RJ";
            var estadoDestino = "RJ";

            notaFiscalItem.CalcularTipoIcms(estadoOrigem, estadoDestino);

            Assert.AreEqual("60", notaFiscalItem.TipoIcms, "Erro ao calcular Tipo do ICMS quando estado de Origem e Destino são iguais.");
        }

        /// <summary>
        /// Testa o calculo do Tipo do ICMS quando estado de Origem e Destino são diferentes.
        /// </summary>
        [TestMethod]
        public void CalcularTipoIcmsEstadoOrigemDestinoDiferentesTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            var estadoOrigem = "RJ";
            var estadoDestino = "SP";

            notaFiscalItem.CalcularTipoIcms(estadoOrigem, estadoDestino);

            Assert.AreEqual("10", notaFiscalItem.TipoIcms, "Erro ao calcular Tipo do ICMS quando estado de Origem e Destino são diferentes.");
        }

        /// <summary>
        /// Testa o calculo do Tipo do ICMS quando estado de Origem e Destino são nulos.
        /// </summary>
        [TestMethod]
        public void CalcularTipoIcmsEstadoOrigemDestinoNulosTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            string estadoOrigem = null;
            string estadoDestino = null;

            notaFiscalItem.CalcularTipoIcms(estadoOrigem, estadoDestino);

            Assert.AreEqual(null, notaFiscalItem.TipoIcms, "Erro ao calcular Tipo do ICMS quando estado de Origem e Destino são nulos.");
        }

        #endregion

        #region Calcular Aliquota ICMS

        /// <summary>
        /// Testa o calculo da Aliquota de ICMS quando estado de Origem e Destino são iguais.
        /// </summary>
        [TestMethod]
        public void CalcularAliquotaIcmsEstadoOrigemDestinoIguaisTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            var estadoOrigem = "RJ";
            var estadoDestino = "RJ";

            notaFiscalItem.CalcularAliquotaIcms(estadoOrigem, estadoDestino);

            Assert.AreEqual((decimal)0.18, notaFiscalItem.AliquotaIcms, "Erro ao calcular Aliquota de ICMS quando estado de Origem e Destino são iguais.");
        }

        /// <summary>
        /// Testa o calculo da Aliquota de ICMS quando estado de Origem e Destino são diferentes.
        /// </summary>
        [TestMethod]
        public void CalcularAliquotaIcmsEstadoOrigemDestinoDiferentesTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            var estadoOrigem = "RJ";
            var estadoDestino = "SP";
            
            notaFiscalItem.CalcularAliquotaIcms(estadoOrigem, estadoDestino);

            Assert.AreEqual((decimal)0.17, notaFiscalItem.AliquotaIcms, "Erro ao calcular Aliquota de ICMS quando estado de Origem e Destino são diferentes.");
        }

        /// <summary>
        /// Testa o calculo da Aliquota de ICMS quando estado de Origem e Destino são nulos.
        /// </summary>
        [TestMethod]
        public void CalcularAliquotaIcmsEstadoOrigemDestinoNulosTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            string estadoOrigem = null;
            string estadoDestino = null;

            notaFiscalItem.CalcularAliquotaIcms(estadoOrigem, estadoDestino);

            Assert.AreEqual(null, notaFiscalItem.AliquotaIcms, "Erro ao calcular Aliquota de ICMS quando estado de Origem e Destino são nulos.");
        }

        #endregion

        #region Calcular Base ICMS

        /// <summary>
        /// Testa o calculo da Base do ICMS quando estado diferente de Sergipe.
        /// </summary>
        [TestMethod]
        public void CalcularBaseIcmsEstadoDiferenteSergipeTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.Cfop = "6.000";
            notaFiscalItem.BaseIcms = 100;

            notaFiscalItem.CalcularBaseIcms();

            Assert.AreEqual(100, notaFiscalItem.BaseIcms, "Erro ao calcular Base do ICMS quando estado diferente de Sergipe.");
        }

        /// <summary>
        /// Testa o calculo da Base do ICMS quando estado igual a Sergipe.
        /// </summary>
        [TestMethod]
        public void CalcularBaseIcmsEstadoIgualSergipeTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.Cfop = "6.009";
            notaFiscalItem.BaseIcms = 100;

            notaFiscalItem.CalcularBaseIcms();

            Assert.AreEqual((decimal)90, notaFiscalItem.BaseIcms, "Erro ao calcular Base do ICMS quando estado igual de Sergipe.");
        }

        /// <summary>
        /// Testa o calculo da Base do ICMS quando CFOP nulo.
        /// </summary>
        [TestMethod]
        public void CalcularBaseIcmsCfopNuloTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.Cfop = null;
            notaFiscalItem.BaseIcms = 100;

            notaFiscalItem.CalcularBaseIcms();

            Assert.AreEqual(notaFiscalItem.BaseIcms, notaFiscalItem.BaseIcms, "Erro ao calcular Base do ICMS quando CFOP nulo.");
        }

        /// <summary>
        /// Testa o calculo da Base do ICMS quando estado igual a Sergipe e Base ICMS nulo.
        /// </summary>
        [TestMethod]
        public void CalcularBaseIcmsEstadoIgualSergipeBaseIcmsNuloTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.Cfop = "6.009";
            notaFiscalItem.BaseIcms = null;

            notaFiscalItem.CalcularBaseIcms();

            Assert.AreEqual(null, notaFiscalItem.BaseIcms, "Erro ao calcular Base do ICMS quando quando estado igual a Sergipe e Base ICMS nulo.");
        }

        #endregion

        #region Calcular Valor ICMS

        /// <summary>
        /// Testa o calculo do Valor do ICMS.
        /// </summary>
        [TestMethod]
        public void CalcularValorIcmsTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.BaseIcms = 100;
            notaFiscalItem.AliquotaIcms = (decimal)0.18;

            notaFiscalItem.CalcularValorIcms();

            Assert.AreEqual(18, notaFiscalItem.ValorIcms, "Erro ao calcular Valor ICMS.");
        }

        /// <summary>
        /// Testa o calculo do Valor do ICMS quando Base ICMS nulo.
        /// </summary>
        [TestMethod]
        public void CalcularValorIcmsBaseIcmsNuloTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.BaseIcms = null;
            notaFiscalItem.AliquotaIcms = (decimal)0.18;

            notaFiscalItem.CalcularValorIcms();

            Assert.AreEqual(null, notaFiscalItem.ValorIcms, "Erro ao calcular Valor ICMS quando Base ICMS nulo.");
        }

        /// <summary>
        /// Testa o calculo do Aliquota do ICMS quando Base ICMS nulo.
        /// </summary>
        [TestMethod]
        public void CalcularValorIcmsAliquotaIcmsNuloTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.BaseIcms = 100;
            notaFiscalItem.AliquotaIcms = null;

            notaFiscalItem.CalcularValorIcms();

            Assert.AreEqual(null, notaFiscalItem.ValorIcms, "Erro ao calcular Aliquota ICMS quando Base ICMS nulo.");
        }

        #endregion

        #region Calcular Item Pedido Brinde

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

            Assert.AreEqual("60", notaFiscalItem.TipoIcms, "Erro ao alterar TipoIcms para 60.");
            Assert.AreEqual((decimal)0.18, notaFiscalItem.AliquotaIcms, "Erro ao alterar AliquotaIcms para 0.18.");
            Assert.AreEqual(18, notaFiscalItem.ValorIcms, "Erro ao alterar o ValorIcms para 18.");
            Assert.AreEqual(0, notaFiscalItem.AliquotaIpi, "Erro ao alterar a AliquotaIpi para 0.");
        }

        #endregion

        #region Calcular Valor IPI

        /// <summary>
        /// Testa o calculo do Valor do IPI.
        /// </summary>
        [TestMethod]
        public void CalcularValorIpiTest()
        {
            notaFiscalItem = new NotaFiscalItem();

            notaFiscalItem.BaseCalculoIpi = 100;
            notaFiscalItem.AliquotaIpi = (decimal)0.10;

            notaFiscalItem.CalcularValorIpi();

            Assert.AreEqual(10, notaFiscalItem.ValorIpi, "Confere se o ValorIpi foi calculado para 10.");
        }

        #endregion

        #region MyRegion

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

        #endregion

    }
}
