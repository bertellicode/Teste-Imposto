using System;
using System.Data.SqlClient;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Repositories;
using Imposto.Infra.CrossCutting.Util;
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
                    new SqlParameter("@pCfop", notaFiscalItem.Cfop.GetValueOrDBNull()),
                    new SqlParameter("@pTipoIcms", notaFiscalItem.TipoIcms.GetValueOrDBNull()),
                    new SqlParameter("@pBaseIcms", notaFiscalItem.BaseIcms.GetValueOrDBNull()),
                    new SqlParameter("@pAliquotaIcms", notaFiscalItem.AliquotaIcms.GetValueOrDBNull()),
                    new SqlParameter("@pValorIcms", notaFiscalItem.ValorIcms.GetValueOrDBNull()),
                    new SqlParameter("@pNomeProduto", notaFiscalItem.NomeProduto.GetValueOrDBNull()),
                    new SqlParameter("@pCodigoProduto", notaFiscalItem.CodigoProduto.GetValueOrDBNull()),
                    new SqlParameter("@pBaseCalculoIpi", notaFiscalItem.BaseCalculoIpi.GetValueOrDBNull()),
                    new SqlParameter("@pAliquotaIpi", notaFiscalItem.AliquotaIpi.GetValueOrDBNull()),
                    new SqlParameter("@pValorIpi", notaFiscalItem.ValorIpi.GetValueOrDBNull()),
                    new SqlParameter("@pDesconto", notaFiscalItem.Desconto.GetValueOrDBNull())
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
