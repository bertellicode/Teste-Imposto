using System;
using System.Data.SqlClient;
using Imposto.Domain.Entities;
using Imposto.Domain.Interfaces.Repositories;
using Imposto.Infra.Data.Contexto;

namespace Imposto.Infra.Data.Repositories
{
    public class NotaFiscalItemRepository : RepositoryBase<NotaFiscalItem>, INotaFiscalItemRepository
    {
        public NotaFiscalItemRepository(TesteImpostoContext context) : base(context)
        {
        }

        public bool Salvar(NotaFiscalItem notaFiscalItem)
        {
            try
            {
                _Db.Database.ExecuteSqlCommand(
                    "P_NOTA_FISCAL_ITEM @pId,@pIdNotaFiscal,@pCfop,@pTipoIcms,@pBaseIcms,@pAliquotaIcms,@pValorIcms,@pNomeProduto,@pCodigoProduto,@pBaseCalculoIpi,@pAliquotaIpi,@pValorIpi,@pDesconto",
                    new SqlParameter("@pId", notaFiscalItem.Id),
                    new SqlParameter("@pIdNotaFiscal", notaFiscalItem.IdNotaFiscal),
                    new SqlParameter("@pCfop", notaFiscalItem.Cfop),
                    new SqlParameter("@pTipoIcms", notaFiscalItem.TipoIcms),
                    new SqlParameter("@pBaseIcms", notaFiscalItem.BaseIcms),
                    new SqlParameter("@pAliquotaIcms", notaFiscalItem.AliquotaIcms),
                    new SqlParameter("@pValorIcms", notaFiscalItem.ValorIcms),
                    new SqlParameter("@pNomeProduto", notaFiscalItem.NomeProduto),
                    new SqlParameter("@pCodigoProduto", notaFiscalItem.CodigoProduto),
                    new SqlParameter("@pBaseCalculoIpi", notaFiscalItem.BaseCalculoIpi),
                    new SqlParameter("@pAliquotaIpi", notaFiscalItem.AliquotaIpi),
                    new SqlParameter("@pValorIpi", notaFiscalItem.ValorIpi),
                    new SqlParameter("@pDesconto", notaFiscalItem.Desconto)
                );
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}
