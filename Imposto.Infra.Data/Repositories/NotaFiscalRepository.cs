using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Imposto.Domain.NotaFiscalAggregate.DTOs;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Domain.NotaFiscalAggregate.Interfaces.Repositories;
using Imposto.Infra.CrossCutting.Util;
using Imposto.Infra.Data.Contexto;

namespace Imposto.Infra.Data.Repositories
{
    public class NotaFiscalRepository : RepositoryBase<NotaFiscal>, INotaFiscalRepository
    {
        public NotaFiscalRepository(TesteImpostoContext context) : base(context)
        {
        }


        public bool SalvarXml(NotaFiscalXmlDto notaFiscal)
        {
            string nome = String.Format("{0}{1}_{2}.xml", "D://", notaFiscal.NumeroNotaFiscal, DateTime.Now.ToString("dd-MM-yyyy_HH-mm"));
            try
            {
                FileStream fs = new FileStream(nome, FileMode.OpenOrCreate);
                XmlSerializer ser = new XmlSerializer(typeof(NotaFiscalXmlDto));
                ser.Serialize(fs, notaFiscal);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

  
        public int? Salvar(NotaFiscal notaFiscal)
        {
            var resultParameter = new SqlParameter("@pId", notaFiscal.Id)
            {
                Direction = ParameterDirection.InputOutput
            };

            try
            {
                var id = _Db.Database.ExecuteSqlCommand(
                    "P_NOTA_FISCAL @pId OUT,@pNumeroNotaFiscal,@pSerie,@pNomeCliente,@pEstadoDestino,@pEstadoOrigem",
                    resultParameter,
                    new SqlParameter("@pNumeroNotaFiscal", notaFiscal.NumeroNotaFiscal.GetValueOrDBNull()),
                    new SqlParameter("@pSerie", notaFiscal.Serie.GetValueOrDBNull()),
                    new SqlParameter("@pNomeCliente", notaFiscal.NomeCliente.GetValueOrDBNull()),
                    new SqlParameter("@pEstadoDestino", notaFiscal.EstadoDestino.GetValueOrDBNull()),
                    new SqlParameter("@pEstadoOrigem", notaFiscal.EstadoOrigem.GetValueOrDBNull())
                ); 
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
                return null;
            }

            return (int?)resultParameter.Value;
        }
    }
}
