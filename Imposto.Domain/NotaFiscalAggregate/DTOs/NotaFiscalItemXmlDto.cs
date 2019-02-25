namespace Imposto.Domain.NotaFiscalAggregate.DTOs
{
    public class NotaFiscalItemXmlDto
    {
        public int Id { get; set; }
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

        public NotaFiscalXmlDto NotaFiscal { get; set; }
    }
}
