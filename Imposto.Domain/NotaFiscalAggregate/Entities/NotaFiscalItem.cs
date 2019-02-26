using System;
using System.Collections.Generic;
using System.Globalization;
using Imposto.Domain.Core.Entities;
using Imposto.Domain.NotaFiscalAggregate.Enums;

namespace Imposto.Domain.NotaFiscalAggregate.Entities
{
    public class NotaFiscalItem : Entity<NotaFiscal>
    {
        public int IdNotaFiscal { get; set; }
        public string Cfop { get; set; }
        public string TipoIcms { get; set; }
        public decimal? BaseIcms { get; set; }
        public decimal? AliquotaIcms { get; set; }
        public decimal? ValorIcms { get; set; }
        public string NomeProduto { get; set; }
        public string CodigoProduto { get; set; }
        public decimal? BaseCalculoIpi { get; set; }
        public decimal? AliquotaIpi { get; set; }
        public decimal? ValorIpi { get; set; }
        public decimal? Desconto { get; set; }

        public NotaFiscal NotaFiscal { get; set; }

        /// <summary>
        /// Responsável por calcular o valor do CFOP.
        /// </summary>
        /// <param name="estadoDestino">Estado de destino da Nota Fiscal.</param>
        public void CalcularCfopPorEstado(string estadoDestino)
        {
            var cfop = RecuperarCfopPorEstado(estadoDestino);

            if (!string.IsNullOrEmpty(cfop))
                Cfop = cfop;
        }

        /// <summary>
        /// Responsável por recuperar o valor do CFOP com base na chave que representa o Estado
        /// </summary>
        /// <param name="estadoDestino">Sigla do Estado Federativo no formato de texto</param>
        /// <returns></returns>
        public string RecuperarCfopPorEstado(string estadoDestino)
        {
            EstadoCfopEnum cfop;
            var sucesso = Enum.TryParse(estadoDestino, out cfop);

            if (sucesso)
            {
                var enumValue = (int)cfop;
                CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");

                return enumValue.ToString("0,0", elGR);
            }

            return null;
        }

        /// <summary>
        /// Responsável por calcular o valor do Tipo do ICMS.
        /// </summary>
        /// <param name="estadoOrigem">Estado de origem da Nota Fiscal.</param>
        /// <param name="estadoDestino">Estado de destino da Nota Fiscal.</param>
        public void CalcularTipoIcms(string estadoOrigem, string estadoDestino)
        {
            if(!string.IsNullOrEmpty(estadoOrigem) && !string.IsNullOrEmpty(estadoDestino))
                TipoIcms = estadoOrigem == estadoDestino ? "60" : "10";
        }

        /// <summary>
        /// Responsável por calcular o valor a Aliquota do ICMS.
        /// </summary>
        /// <param name="estadoOrigem">Estado de origem da Nota Fiscal.</param>
        /// <param name="estadoDestino">Estado de destino da Nota Fiscal.</param>
        public void CalcularAliquotaIcms(string estadoOrigem, string estadoDestino)
        {
            if (!string.IsNullOrEmpty(estadoOrigem) && !string.IsNullOrEmpty(estadoDestino))
                AliquotaIcms = estadoOrigem == estadoDestino ? (decimal)0.18 : (decimal)0.17;
        }

        /// <summary>     
        /// Responsável por calcular a base do ICMS. 
        /// </summary>
        public void CalcularBaseIcms()
        {
            var cfopSergipe = RecuperarCfopPorEstado(EstadoCfopEnum.SE.ToString());
            BaseIcms = Cfop == cfopSergipe ? BaseIcms * (decimal)0.90 : BaseIcms;
        }

        /// <summary>
        /// Responsável por calcular o valor do ICMS. 
        /// </summary>
        public void CalcularValorIcms()
        {
            ValorIcms = BaseIcms * AliquotaIcms;
        }

        /// <summary>
        /// Responsável por calcular os valores do Tipo ICMS, Aliquota ICMS, Valor ICMS e Aliquota do IPI 
        /// com base no brinde do item do pedido. 
        /// </summary>
        /// <param name="brinde">Brinde passado no item do pedido.</param>
        public void CalcularItemPedidoBrinde(bool brinde)
        {
            if (brinde)
            {
                TipoIcms = "60";
                AliquotaIcms = (decimal)0.18;
                AliquotaIpi = 0;
                CalcularValorIcms();
            }
        }

        /// <summary>
        /// Responsável por calcular o Valor do IPI com base na Base de Cálculo do IPI
        /// e na Aliquota do IPI
        /// </summary>
        public void CalcularValorIpi()
        {
            ValorIpi = BaseCalculoIpi * AliquotaIpi;
        }

        /// <summary>
        /// Responsável por calcular o percentual de Desconto a ser aplicado.
        /// </summary>
        /// <param name="estadoDestino">Estado de destino da Nota Fiscal.</param>
        public void CalcularPercentualDesconto(string estadoDestino)
        {
            var estadosSudeste = new List<string>{ "RJ", "SP", "MG", "ES" };
            if (estadosSudeste.Contains(estadoDestino))
            {
                Desconto = (decimal)0.10;
            }
        }

        /// <summary>
        /// Validar Item da Nota Fiscal
        /// </summary>
        /// <returns>Status da oepração de validação</returns>
        public override bool Validar()
        {
            throw new NotImplementedException();
        }
    }
}
