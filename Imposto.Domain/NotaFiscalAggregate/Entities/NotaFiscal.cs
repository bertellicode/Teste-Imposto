using FluentValidation;
using Imposto.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Imposto.Domain.NotaFiscalAggregate.Entities
{
    public class NotaFiscal : Entity<NotaFiscal>
    {
        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }

        public int? NumeroNotaFiscal { get; set; }
        public int? Serie { get; set; }
        public string NomeCliente { get; set; }
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public virtual List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public bool AdicionarItemDaNotaFiscal(NotaFiscalItem notaFiscalItem, bool brinde)
        {
            if (string.IsNullOrEmpty(EstadoOrigem) && string.IsNullOrEmpty(EstadoDestino))
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

        private void ValidarNomeCliente()
        {
            RuleFor(x => x.NomeCliente)
                .Must(x => !string.IsNullOrEmpty(NomeCliente))
                .WithMessage("Obrigatório informar o Nome do Cliente!");
        }

        private void ValidarEstadoOrigem()
        {
            RuleFor(x => x.EstadoOrigem)
                .Must(x => !string.IsNullOrEmpty(EstadoOrigem))
                .WithMessage("Obrigatório informar o Estado Origem!");
        }

        private void ValidarEstadoDestino()
        {
            RuleFor(x => x.EstadoDestino)
                .Must(x => !string.IsNullOrEmpty(EstadoDestino))
                .WithMessage("Obrigatório informar o Estado Destino!");
        }

        private void ValidarItemNotaFiscal()
        {
            RuleFor(x => x.ItensDaNotaFiscal)
                .Must(x => ItensDaNotaFiscal != null && ItensDaNotaFiscal.Any())
                .WithMessage("Obrigatório informar ao menos um Item da Nota Fiscal!");
        }

        public override bool Validar()
        {
            ValidarNomeCliente();
            ValidarEstadoOrigem();
            ValidarEstadoDestino();
            ValidarItemNotaFiscal();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
