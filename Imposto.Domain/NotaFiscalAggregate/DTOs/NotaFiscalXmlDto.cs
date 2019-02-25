using System.Collections.Generic;

namespace Imposto.Domain.NotaFiscalAggregate.DTOs
{
    public class NotaFiscalXmlDto
    {
        public NotaFiscalXmlDto()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItemXmlDto>();
        }

        public int Id { get; set; }
        public int? NumeroNotaFiscal { get; set; }
        public int? Serie { get; set; }
        public string NomeCliente { get; set; }
        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        public List<NotaFiscalItemXmlDto> ItensDaNotaFiscal { get; set; }
    }
}
