using System;
using System.Collections.Generic;

namespace Imposto.Domain.NotaFiscalAggregate.Entities
{
    public class NotaFiscal
    {
        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }

        public int Id { get; set; }
        public int? NumeroNotaFiscal { get; set; }
        public int? Serie { get; set; }
        public string NomeCliente { get; set; }
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public virtual List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public bool AdicionarItemDaNotaFiscal(NotaFiscalItem notaFiscalItem, bool brinde)
        {
            if (!ValidarEstados())
                return false;

            notaFiscalItem.CalcularCfopPorEstado(EstadoDestino);

            notaFiscalItem.CalcularTipoIcms(EstadoOrigem, EstadoDestino);

            notaFiscalItem.CalcularAliquotaIcms(EstadoOrigem, EstadoDestino);

            notaFiscalItem.CalcularBaseIcms();

            notaFiscalItem.CalcularValorIcms();

            notaFiscalItem.CalcularItemPedidoBrinde(brinde);

            notaFiscalItem.CalcularValorIpi();

            notaFiscalItem.CalcularPercentualDesconto(EstadoDestino);

            ItensDaNotaFiscal.Add(notaFiscalItem);

            return true;
        }

        private bool ValidarEstados()
        {
            return ValidarEstadoOrigem() && ValidarEstadoDestino();
        }

        private bool ValidarEstadoOrigem()
        {
            return !string.IsNullOrEmpty(EstadoOrigem);
        }

        private bool ValidarEstadoDestino()
        {
            return !string.IsNullOrEmpty(EstadoDestino);
        }
    }
}
