using Imposto.Domain;

namespace Imposto.Service
{
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);
        }
    }
}
