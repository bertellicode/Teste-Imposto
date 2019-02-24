using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Imposto.Domain.NotaFiscalAggregate.Entities;
using Imposto.Infra.Data.EntityConfig;

namespace Imposto.Infra.Data.Contexto
{
    public class TesteImpostoContext : DbContext
    {

        public TesteImpostoContext() : base("Teste")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<NotaFiscal> NotaFiscal { get; set; }
        public DbSet<NotaFiscalItem> NotaFiscalItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new NotaFiscalConfiguration());
            modelBuilder.Configurations.Add(new NotaFiscalItemConfiguration());
        }

    }
}
